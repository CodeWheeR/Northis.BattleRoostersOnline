using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Enums;
using Unity.ServiceLocation;
using Unity;

namespace Northis.BattleRoostersOnline.Implements
{
	public class AuthenticateService : BaseServiceWithStorage, IAuthenticateService
	{
		private Random _rand = new Random();
		private BinaryFormatter formatter = new BinaryFormatter();


		public string LogIn(string login, string password)
		{
			if (!(Storage.UserData).ContainsKey(login) || Storage.UserData[login] != Encrypt(password))
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			if (Storage.LoggedUsers.ContainsValue(login))
				return AuthenticateStatus.AlreadyLoggedIn.ToString();

			var token = GenerateToken();
			Storage.LoggedUsers.Add(token, login);
			return token;
		}

		public string Register(string login, string password)
		{
			if (Storage.UserData.ContainsKey(login))
				return AuthenticateStatus.AlreadyRegistered.ToString();
			Storage.UserData.Add(login, Encrypt(password));
			return LogIn(login, password);
		}

		public bool LogOut(string token)
		{
			var data = Storage.LoggedUsers;
			if (!data.ContainsKey(token))
				return false;

			data.Remove(token);
			return true;
		}

		private string GenerateToken()
		{
			string _tokenGeneratorSymbols = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string answer = "";
			for (int i = 0; i < 16; i++)
			{
				answer += _tokenGeneratorSymbols[_rand.Next(0, _tokenGeneratorSymbols.Length - 1)];
			}

			return answer;
		}

		public string Encrypt(string sourceString)
		{
			string result = "";
			for (int i = 0; i < sourceString.Length; i++)
			{
				result += (char)(sourceString[i] * (i/2+2));
			}

			return result;
		}

		public string Decrypt(string sourceString)
		{
			string result = "";
			for (int i = 0; i < sourceString.Length; i++)
			{
				result += (char)(sourceString[i] / (i / 2 + 2));
			}

			return result;
		}

		public void SaveUserData()
		{
			using (FileStream fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, Storage.UserData);
			}
		}

		public Dictionary<string, string> LoadUserData()
		{
			using (FileStream fs = new FileStream("users.dat", FileMode.Open))
			{
				return (Dictionary<string, string>)formatter.Deserialize(fs);
			}
		}
	}
}
