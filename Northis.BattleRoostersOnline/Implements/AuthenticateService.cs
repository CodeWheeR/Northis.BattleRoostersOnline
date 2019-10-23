using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using static System.String;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Предоставляет сервис аунтефикации.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Implements.BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IAuthenticateService" />
	public class AuthenticateService : BaseServiceWithStorage, IAuthenticateService
	{
		#region Fields
		#region Private

		private Thread _connectionMonitor;

		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthenticateService" /> класса.
		/// </summary>
		public AuthenticateService()
		{
			if (Storage.UserData.Count == 0)
			{
				Storage.LoadUserData();
			}
		}
		#endregion

		#region Methods
		#region Public

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> LogInAsync(string login, string password) => await LogInAsync(login, password, false);

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> LogInAsync(string login, string password, bool isEncrypted = false)
		{
			string token;
			var callback = OperationContext.Current.GetCallbackChannel<IAuthenticateServiceCallback>();

			if (!isEncrypted)
				password = await EncryptAsync(password);


			if (!Storage.UserData.ContainsKey(login) || Storage.UserData[login] != password)
			{
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			}

			if (Storage.LoggedUsers.ContainsValue(login))
			{
				return AuthenticateStatus.AlreadyLoggedIn.ToString();
			}

			token = await GenerateTokenAsync();
			await Task.Run(() =>
			{
				lock (Storage.UserData)
				{
					Storage.LoggedUsers.Add(token, login);
				}
			});

			(await StatisticsPublisher.GetInstanceAsync())
							   .Subscribe(token, callback);
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
		public async Task<string> RegisterAsync(string login, string password)
		{
			if (login.Length < 5 || IsNullOrWhiteSpace(login) || password.Length < 5 || IsNullOrWhiteSpace(password) || login.Contains(" "))
			{
				return AuthenticateStatus.WrongDataFormat.ToString();
			}

			if (Storage.UserData.ContainsKey(login))
			{
				return AuthenticateStatus.AlreadyRegistered.ToString();
			}

			var encryptedPassword = await EncryptAsync(password);

			await Task.Run(() =>
			{
				lock (Storage.UserData)
				{
					Storage.UserData.Add(login, encryptedPassword);
				}
			});
#pragma warning disable 4014
			Storage.SaveUserDataAsync();
#pragma warning restore 4014
			return await LogInAsync(login, encryptedPassword, true);
		}

		/// <summary>
		/// Осуществляет выход пользователя из системы.
		/// </summary>
		/// <param name="token">токен.</param>
		/// <returns>
		/// true - в случае успешного выхода, иначе - false.
		/// </returns>
		public async Task<bool> LogOutAsync(string token)
		{
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				return false;
			}

			await Task.Run(() =>
			{
				lock (Storage.UserData)
				{
					Storage.LoggedUsers.Remove(token);
				}
			});

			await Task.Run(async () => (await StatisticsPublisher.GetInstanceAsync())
													.Unsubscribe(token));

			return true;
		}

		/// <summary>
		/// Возвращает статус авторизации пользователя.
		/// </summary>
		/// <returns>
		/// AuthenticateStatus.
		/// </returns>
		public AuthenticateStatus GetLoginStatus() => AuthenticateStatus.OK;

		/// <summary>
		/// Зашифровывает исходную строку.
		/// </summary>
		/// <param name="sourceString">исходная строка.</param>
		/// <returns>Зашифрованная строка.</returns>
		public async Task<string> EncryptAsync(string sourceString)
		{
			return await Task.Run<string>(() =>
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
		/// Расшифровывает поступившую зашифрованную строку.
		/// </summary>
		/// <param name="sourceString">Зашифрованная строка.</param>
		/// <returns>Расшифрованная строка.</returns>
		public string Decrypt(string sourceString)
		{
			var result = "";
			for (var i = 0; i < sourceString.Length; i++)
			{
				result += (char) (sourceString[i] / (i / 2 + 2));
			}

			return result;
		}
		
		/// <summary>
		/// Асинхронно собирает глобальную статистику по пользователям.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<StatisticsDto>> GetGlobalStatisticsAsync()
		{
			return await Task.Run<IEnumerable<StatisticsDto>>(async () => (await StatisticsPublisher.GetInstanceAsync())
																					   .GetGlobalStatistics());
		}
		#endregion
		#endregion
	}
}
