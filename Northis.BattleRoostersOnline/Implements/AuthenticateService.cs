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

		public Dictionary<string, string> LogData
		{
			get
			{	
				if (ServiceLocator.IsLocationProviderSet)
					return (Dictionary<string, string>) ServiceLocator.Current.GetInstance<ServicesStorage>()
														   .UserData;
				throw new NullReferenceException("Storage is null");
			}
		}

		public AuthenticateService()
		{
			if (!ServiceLocator.IsLocationProviderSet)
				ServiceLocator.SetLocatorProvider(() =>
				{
					var container = new UnityContainer();
					container.RegisterInstance<ServicesStorage>(new ServicesStorage());
					return new UnityServiceLocator(container);
				});
		}

		public string LogIn(string login, string password)
		{
			if (!LogData.ContainsKey(login) || LogData[login] != Encrypt(password))
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			var token = GenerateToken();

			((Dictionary<string, List<RoosterDto>>) ServiceLocator.Current.GetInstance<ServicesStorage>()
														.RoosterData).Add(token, null);
			return token;
		}

		public string Register(string login, string password)
		{
			if (LogData.ContainsKey(login))
				return AuthenticateStatus.AlreadyRegistered.ToString();
			LogData.Add(login, Encrypt(password));
			return LogIn(login, Decrypt(password));
		}

		public bool LogOut(string token)
		{
			var data = ((Dictionary<string, List<RoosterDto>>) ServiceLocator.Current.GetInstance<ServicesStorage>()
																   .RoosterData);
			if (!data.ContainsKey(token))
				return false;

			data.Remove(token);
			return true;
		}

		public string GenerateToken()
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
			BinaryFormatter formatter = new BinaryFormatter();
			using (FileStream fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, LogData);
			}
		}

		public IEnumerable<KeyValuePair<string, string>> LoadUserData()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			using (FileStream fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				return (Dictionary<string, string>)formatter.Deserialize(fs);
			}
		}
	}
}
