using System.Collections.Generic;
using System.IO;
using System.Net;
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
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <param name="callback"></param>
		/// <param name="isEncrypted"></param>
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

			token = await GenerateTokenAsync();
			await Task.Run(() =>
			{
				lock (StorageService.UserData)
				{
					StorageService.LoggedUsers.Add(token, login);
				}
			});

			StatisticsPublisher.GetInstance().Subscribe(token, callback);
			return token;
		}

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
			if (IsNullOrWhiteSpace(login) || login.Length < 5  || IsNullOrWhiteSpace(password) || password.Length < 5 || login.Contains(" "))
			{
				return AuthenticateStatus.WrongDataFormat.ToString();
			}

			if (StorageService.UserData.ContainsKey(login))
			{
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
#pragma warning disable 4014
			StorageService.SaveUserDataAsync();
#pragma warning restore 4014
			return await LogInAsync(login, encryptedPassword, callback, true);
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
			if (!StorageService.LoggedUsers.ContainsKey(token))
			{
				return false;
			}

			await Task.Run(() =>
			{
				lock (StorageService.UserData)
				{
					StorageService.LoggedUsers.Remove(token);
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
	}
}
