using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Properties;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Предоставляет сервис аутентификации.
	/// </summary>
	/// <seealso cref="BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IAuthenticateService" />
	public class AuthenticateService : BaseServiceWithStorage, IAuthenticateService
	{
		#region Private Fields
		private Logger _logger = LogManager.GetCurrentClassLogger();
		private CancellationTokenSource _connectionMonitorTokenSource = new CancellationTokenSource();
        #endregion

        #region .ctor        
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AuthenticateService"/> класса.
        /// </summary>
        /// <param name="storage">Объект хранилища. </param>
        public AuthenticateService(IDataStorageService storage) : base(storage)
		{
			StatisticsPublisher.GetInstance(StorageService);
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> LogInAsync(string login, string password) => await LogInAsync(login, password, OperationContext.Current.GetCallbackChannel<IAuthenticateServiceCallback>(), false);
        /// <summary>
        /// Осуществляет проверку состояния подключения клиентов.
        /// </summary>
        public async Task MonitorConnections()
		{
			var token = _connectionMonitorTokenSource.Token;

			await Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					StatisticsPublisher.GetInstance()
									   .UpdateStatistics();
					await Task.Delay(10000, token);
				}
			}, token);
		}

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <param name="callback">Метод оповещения клиента.</param>
		/// <param name="isEncrypted">Показатель зашифрованности поступившего пароля.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> LogInAsync(string login, string password, IAuthenticateServiceCallback callback,  bool isEncrypted = false)
		{
			string token = "";

			if (!isEncrypted)
				password = await EncryptAsync(password);


			if (!StorageService.UserData.ContainsKey(login) || StorageService.UserData[login] != password)
			{
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			}

			if (StorageService.LoggedUsers.ContainsValue(login))
			{
				return AuthenticateStatus.AlreadyLoggedIn.ToString();
			}

			token = await GenerateTokenAsync(StorageService.LoggedUsers.ContainsKey);
			await Task.Run(() =>
			{
				lock (StorageService.UserData)
				lock(StorageService.LoggedUsers)
				{
					StorageService.LoggedUsers.Add(token, login);
					if (StorageService.LoggedUsers.Count == 1)
					{
						MonitorConnections();
					}
				}
			});

			_logger.Info(Resources.StrFmtInfoUserLogined, login, token);

			StatisticsPublisher.GetInstance().Subscribe(token, callback);
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();
			return token;
		}
        /// <summary>
        /// Регистрирует нового пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>
        /// Токен.
        /// </returns>
        public async Task<string> RegisterAsync(string login, string password) =>
			await RegisterAsync(login, password, OperationContext.Current.GetCallbackChannel<IAuthenticateServiceCallback>());

		/// <summary>
		/// Регистрирует нового пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> RegisterAsync(string login, string password, IAuthenticateServiceCallback callback)
		{
			if (string.IsNullOrWhiteSpace(login) || login.Length < 5  || string.IsNullOrWhiteSpace(password) || password.Length < 5 || login.Contains(" "))
			{
				_logger.Info(Resources.StrFmtInfoTryLoginNotRegistredUser,login);
				return AuthenticateStatus.WrongDataFormat.ToString();
			}

			if (StorageService.UserData.ContainsKey(login))
			{
				_logger.Info(Resources.StrFmtInfoTryRegisterAlreadyRegistredUser, login);
				return AuthenticateStatus.AlreadyRegistered.ToString();
			}

			var encryptedPassword = await EncryptAsync(password);

			await Task.Run(() =>
			{
				lock (StorageService.UserData)
				{
					StorageService.UserData.Add(login, encryptedPassword);
				}
			});

			_logger.Info(Resources.StrFmtInfoSuccessRegitstration, login);
#pragma warning disable 4014
			StorageService.SaveUserDataAsync();
#pragma warning restore 4014
			return await LogInAsync(login, encryptedPassword, callback, true);
		}

		/// <summary>
		/// Осуществляет выход пользователя из системы.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешного выхода, иначе - false.
		/// </returns>
		public async Task<bool> LogOutAsync(string token)
		{
			if (!StorageService.LoggedUsers.ContainsKey(token))
			{
				_logger.Warn(Resources.StrFmtWarnTryDisconnectNotAuthorizedUser, token);
				return false;
			}

			var login = await GetLoginAsync(token);

			await Task.Run(() =>
			{
				lock (StorageService.UserData)
				{
					StorageService.LoggedUsers.Remove(token);
				}
			});

			_logger.Info(Resources.StrFmtInfoUserLogOut, login);

			await Task.Run(() => StatisticsPublisher.GetInstance().Unsubscribe(token));
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();


			return true;
		}

		/// <summary>
		/// Возвращает статус авторизации пользователя.
		/// </summary>
		/// <returns>
		/// Статус аутентификации.
		/// </returns>
		public AuthenticateStatus GetLoginStatus() => AuthenticateStatus.Ok;

		/// <summary>
		/// Зашифровывает исходную строку.
		/// </summary>
		/// <param name="sourceString">Исходная строка.</param>
		/// <returns>Зашифрованная строка.</returns>
		private Task<string> EncryptAsync(string sourceString)
		{
			return Task.Run<string>(() =>
			{
				var result = "";
				for (var i = 0; i < sourceString.Length; i++)
				{
					result += (char)(sourceString[i] * (i / 2 + 2));
				}

				return result;
			});
		}

		/// <summary>
		/// Асинхронно собирает глобальную статистику по пользователям.
		/// </summary>
		/// <returns>Статистика пользователей.</returns>
		public async Task<IEnumerable<StatisticsDto>> GetGlobalStatisticsAsync()
		{
			return await Task.Run<IEnumerable<StatisticsDto>>(async () => (await StatisticsPublisher.GetInstanceAsync())
																					   .GetGlobalStatistics());
		}
		#endregion
	}
}
