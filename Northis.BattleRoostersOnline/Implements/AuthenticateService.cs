using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Enums;

namespace Northis.BattleRoostersOnline.Implements
{
	public class AuthenticateService : IAuthenticateService
	{
		private Random _rand = new Random();
		private Dictionary<string, string> _logData;

		public AuthenticateService()
		{
			_logData = (Dictionary<string, string>) ServiceLocator.Current.GetInstance<ServicesStorage>()
																  .UserData;
		}

		public string LogIn(string login, string password)
		{
			if (!_logData.ContainsKey(login) || _logData[login] != Encrypt(password))
				return AuthenticateStatus.WrongLoginOrPassword.ToString();
			return GenerateToken();
		}

		public string Register(string login, string password, string codePhrase)
		{
			if (_logData.ContainsKey(login))
				return AuthenticateStatus.AlreadyRegistered.ToString();
			_logData.Add(login, Encrypt(password));
			return LogIn(login, Decrypt(password));
		}

		public bool LogOut() => throw new NotImplementedException();

		public string ResetPassword(string username, string codePhrase) => throw new NotImplementedException();

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
				result += sourceString[i] * (i/2+1);
			}

			return result;
		}

		public string Decrypt(string sourceString)
		{
			string result = "";
			for (int i = 0; i < sourceString.Length; i++)
			{
				result += sourceString[i] / (i / 2 + 1);
			}

			return result;
		}
	}
}
