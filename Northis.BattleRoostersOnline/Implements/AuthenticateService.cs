using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
		/// <summary>
		/// Бинарный сериализатор
		/// </summary>
		private readonly BinaryFormatter _formatter = new BinaryFormatter();
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
				LoadUserData();
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
		public async Task<string> LogIn(string login, string password)
		{
			if (!Storage.UserData.ContainsKey(login) || Storage.UserData[login] != Encrypt(password))
			{
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			}

			if (Storage.LoggedUsers.ContainsValue(login))
			{
				return AuthenticateStatus.AlreadyLoggedIn.ToString();
			}

			var token = await GenerateTokenAsync();
			Storage.LoggedUsers.Add(token, login);
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
		public async Task<string> Register(string login, string password)
		{
			if (login.Length < 5 || IsNullOrWhiteSpace(login) || password.Length < 5 || IsNullOrWhiteSpace(password) || login.Contains(" "))
			{
				return AuthenticateStatus.WrongDataFormat.ToString();
			}

			if (Storage.UserData.ContainsKey(login))
			{
				return AuthenticateStatus.AlreadyRegistered.ToString();
			}

			Storage.UserData.Add(login, Encrypt(password));
			SaveUserDataAsync();
			return await LogIn(login, password);
		}

		/// <summary>
		/// Осуществляет выход пользователя из системы.
		/// </summary>
		/// <param name="token">токен.</param>
		/// <returns>
		/// true - в случае успешного выхода, иначе - false.
		/// </returns>
		public async Task<bool> LogOut(string token)
		{
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				return false;
			}

			await Task.Run(() => Storage.LoggedUsers.Remove(token));
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
		public string Encrypt(string sourceString)
		{
			var result = "";
			for (var i = 0; i < sourceString.Length; i++)
			{
				result += (char) (sourceString[i] * (i / 2 + 2));
			}

			return result;
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
		/// Асинхронно сохраняет данные пользователя.
		/// </summary>
		public async Task SaveUserDataAsync()
		{
			await Task.Run(() =>
			{
				if (!Directory.Exists("Resources"))
				{
					Directory.CreateDirectory("Resources");
				}

				using (var fs = new FileStream("Resources\\users.dat", FileMode.OpenOrCreate))
				{
					_formatter.Serialize(fs, Storage.UserData);
				}
			});
		}

		/// <summary>
		/// Загружает данные пользователя.
		/// </summary>
		public void LoadUserData()
		{
			if (File.Exists("Resources\\users.dat"))
			{
				using (var fs = new FileStream("Resources\\users.dat", FileMode.Open))
				{
					Storage.UserData = (Dictionary<string, string>) _formatter.Deserialize(fs);
				}
			}
		}
		#endregion
		#endregion
	}
}
