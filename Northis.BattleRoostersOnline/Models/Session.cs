using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Events;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.Models
{
	public class Session
	{
		private event EventHandler<MatchFindedEventArgs> SessionStarted;

		private CancellationTokenSource _tokenSource = new CancellationTokenSource();

		public IBattleServiceCallback FirstCallback
		{
			get;
			set;
		}

		public IBattleServiceCallback SecondCallback
		{
			get;
			set;
		}

		internal RoosterModel SecondFighter
		{
			get;
			set;
		}

		internal string SecondUserToken
		{
			get;
			set;
		}

		internal RoosterModel FirstFighter
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



		public string Token
		{
			get;
			set;
		}

		public Session(string token)
		{
			Token = token;
		}

		public void RegisterFighter(string token, RoosterDto fighter, IBattleServiceCallback callback)
		{
			if (FirstFighter == null)
			{
				FirstFighter = new RoosterModel(fighter);
				FirstUserToken = token;
				FirstCallback = callback;
				Subscribe(callback);
			}
			else if (!IsStarted)
			{
				IsStarted = true;
				SecondFighter = new RoosterModel(fighter);
				SecondUserToken = token;
				SecondCallback = callback;
				Subscribe(callback);
				SendReadySignAsync();
			}
		}

		private void Subscribe(IBattleServiceCallback callback)
		{
			SessionStarted += (x, y) => callback.FindedMatch(y.MatchToken);
		}

		private void SendReadySignAsync()
		{
			Task.Run(() => SessionStarted?.Invoke(this, new MatchFindedEventArgs(Token)));
			SendRoosterStatus();
		}

		public bool RemoveFighter(string token)
		{
			if (token == FirstUserToken)
				return true;
			return false;
		}

		public void SendStartSignAsync()
		{
			Task.Run(() =>
			{
				FirstCallback.GetStartSign();
				SecondCallback.GetStartSign();
			});
		}

		public void StopBattle()
		{
			_tokenSource.Cancel();
			_tokenSource = new CancellationTokenSource();
		}

		public void StartBattle()
		{
			var token = _tokenSource.Token;

			Task.Run(() =>
			{
				while (FirstFighter.Health > 0 && SecondFighter.Health > 0)
				{
					FirstFighter.TakeHit(SecondFighter);
					SecondFighter.TakeHit(FirstFighter);
					SendRoosterStatus();
					Task.WaitAll(Task.Delay(500, token));
				}

				if (FirstFighter.Health == 0 && SecondFighter.Health == 0)
				{
					FirstFighter.WinStreak = 0;
					SecondFighter.WinStreak = 0;
				}
				else if (FirstFighter.Health == 0)
				{
					FirstFighter.WinStreak = 0;
					SecondFighter.WinStreak++;
				}
				else
				{
					FirstFighter.WinStreak++;
					SecondFighter.WinStreak = 0;
				}

				Task.Run(() => FirstCallback.GetBattleMessage("Матч Окончен"));
				Task.Run(() => SecondCallback.GetBattleMessage("Матч Окончен"));
			});
		}

		private void SendRoosterStatus()
		{
			Task.Run(() => FirstCallback.GetRoosterStatus(FirstFighter.ToRoosterDto(), SecondFighter.ToRoosterDto()));
			Task.Run(() => SecondCallback.GetRoosterStatus(SecondFighter.ToRoosterDto(), FirstFighter.ToRoosterDto()));
		}
	}
}
