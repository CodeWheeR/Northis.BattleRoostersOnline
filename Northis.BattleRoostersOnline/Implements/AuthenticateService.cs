using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using static System.String;

namespace Northis.BattleRoostersOnline.Implements
{
	public class AuthenticateService : BaseServiceWithStorage, IAuthenticateService
	{
		private readonly BinaryFormatter _formatter = new BinaryFormatter();

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

		public async Task<string> Register(string login, string password)
		{
			if (login.Length <= 6 || IsNullOrWhiteSpace(login) ||  password.Length <= 6 || IsNullOrWhiteSpace(password))
			{
				return AuthenticateStatus.WrongDataFormat.ToString();
			}

			if (Storage.UserData.ContainsKey(login))
			{
				return AuthenticateStatus.AlreadyRegistered.ToString();
			}
			
			Storage.UserData.Add(login, Encrypt(password));
			return await LogIn(login, password);
		}

		public async Task<bool> LogOut(string token)
		{
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				return false;
			}

			await Task.Run(() => Storage.LoggedUsers.Remove(token));
			return true;
		}

		public AuthenticateStatus GetLoginStatus() => AuthenticateStatus.OK;

		public string Encrypt(string sourceString)
		{
			var result = "";
			for (var i = 0; i < sourceString.Length; i++)
			{
				result += (char) (sourceString[i] * (i / 2 + 2));
			}

			return result;
		}

		public string Decrypt(string sourceString)
		{
			var result = "";
			for (var i = 0; i < sourceString.Length; i++)
			{
				result += (char) (sourceString[i] / (i / 2 + 2));
			}

			return result;
		}

		public void SaveUserData()
		{
			using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				_formatter.Serialize(fs, Storage.UserData);
			}
		}

		public Dictionary<string, string> LoadUserData()
		{
			using (var fs = new FileStream("users.dat", FileMode.Open))
			{
				return (Dictionary<string, string>) _formatter.Deserialize(fs);
			}
		}
	}
}
