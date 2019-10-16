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
	public class AuthenticateService : IAuthenticateService
	{
		private Random _rand = new Random();
		private BinaryFormatter formatter = new BinaryFormatter();

		private ServicesStorage _servicesStorage
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
					return ServiceLocator.Current.GetInstance<ServicesStorage>();
				throw new NullReferenceException("Storage is null");
			}
		}

		public AuthenticateService()
		{
			if (!ServiceLocator.IsLocationProviderSet)
			{
				var container = new UnityContainer();
				container.RegisterInstance<ServicesStorage>(new ServicesStorage());
				var locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
		}

		public string LogIn(string login, string password)
		{
			if (!(_servicesStorage.UserData).ContainsKey(login) || _servicesStorage.UserData[login] != Encrypt(password))
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			if (_servicesStorage.LoggedUsers.ContainsValue(login))
				return AuthenticateStatus.AlreadyLoggedIn.ToString();

			var token = GenerateToken();
			_servicesStorage.LoggedUsers.Add(token, login);
			return token;
		}

		public string Register(string login, string password)
		{
			if (_servicesStorage.UserData.ContainsKey(login))
				return AuthenticateStatus.AlreadyRegistered.ToString();
			_servicesStorage.UserData.Add(login, Encrypt(password));
			return LogIn(login, password);
		}

		public bool LogOut(string token)
		{
			var data = _servicesStorage.LoggedUsers;
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
				formatter.Serialize(fs, _servicesStorage.UserData);
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
