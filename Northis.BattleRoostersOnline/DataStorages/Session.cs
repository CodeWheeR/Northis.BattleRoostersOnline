using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Events;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.DataStorages
{
	public class Session
	{
		internal RoosterDto SecondFighter
		{
			get;
			set;
		}

		internal string SecondUserToken
		{
			get;
			set;
		}

		internal RoosterDto FirstFighter
		{
			get;
			set;
		}

		internal string FirstUserToken
		{
			get;
			set;
		}

		public bool IsStarted
		{
			get; set;
		}

		private event EventHandler<MatchFindedEventArgs> _sessionStarted;

		public string Token
		{
			get;
			set;
		}

		public Session(string token)
		{
			Token = token;
		}

		public void RegisterFighter(string token, RoosterDto fighter, IFindServiceCallback callback)
		{
			if (FirstFighter == null)
			{
				FirstFighter = fighter;
				FirstUserToken = token;
				Subscribe(callback);
			}
			else if (!IsStarted)
			{
				IsStarted = true;
				SecondFighter = fighter;
				SecondUserToken = token;
				Subscribe(callback);
				SendReadySignAsync();
			}
		}

		private void Subscribe(IFindServiceCallback callback)
		{
			_sessionStarted += (x, y) => callback.FindedMatch(y.MatchToken);
		}

		private void SendReadySignAsync()
		{
			Task.Run(() => _sessionStarted?.Invoke(this, new MatchFindedEventArgs(Token)));
		}

		public bool RemoveFighter(string token)
		{
			if (token == FirstUserToken)
				return true;
			return false;
		}
	}
}
