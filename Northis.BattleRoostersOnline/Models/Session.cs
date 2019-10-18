using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Events;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.Models
{
	public class Session : BaseServiceWithStorage
	{
		private event EventHandler<MatchFindedEventArgs> SessionStarted;

		private CancellationTokenSource _tokenSource = new CancellationTokenSource();

		private bool _firstIsReady;
		private bool _secondIsReady;

		private IBattleServiceCallback _firstCallback;
		private IBattleServiceCallback _secondCallback;

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

		internal bool IsStarted
		{
			get; set;
		}

		internal string Token
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
				_firstCallback = callback;
				Subscribe(callback);
			}
			else if (!IsStarted)
			{
				IsStarted = true;
				SecondFighter = new RoosterModel(fighter);
				SecondUserToken = token;
				_secondCallback = callback;
				Subscribe(callback);
				SendReadySignAsync();
			}
		}

		private void Subscribe(IBattleServiceCallback callback)
		{
			SessionStarted += (x, y) => callback.FindedMatch(y.MatchToken);
		}

		public void SetReady(string token)
		{
			if (token == FirstUserToken)
				_firstIsReady = true;
			else
				_secondIsReady = true;
		}

		public bool CheckForReadiness()
		{
			if (_firstIsReady && _secondIsReady)
				return true;
			return false;
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
				_firstCallback.GetStartSign();
				_secondCallback.GetStartSign();
			});
		}

		public void StopBattle()
		{
			_tokenSource.Cancel();
			_tokenSource = new CancellationTokenSource();
		}

		public async Task StartBattle()
		{
			var token = _tokenSource.Token;

			await Task.Run(async () =>
			{
				while (FirstFighter.Health > 0 && SecondFighter.Health > 0 && !token.IsCancellationRequested)
				{
					FirstFighter.TakeHit(SecondFighter);
					SecondFighter.TakeHit(FirstFighter);
					if (FirstFighter.Health != 0 && SecondFighter.Health != 0)
					{
						SendRoosterStatus();
						await Task.Delay(500, token);
					}
				}

				if (FirstFighter.Health == 0 && SecondFighter.Health == 0)
				{
					await SetWinstreak(FirstUserToken, FirstFighter.Name, 0);
					await SetWinstreak(SecondUserToken, SecondFighter.Name, 0);
				}
				else if (FirstFighter.Health == 0)
				{
					await SetWinstreak(FirstUserToken, FirstFighter.Name, 0);
					await SetWinstreak(SecondUserToken, SecondFighter.Name, SecondFighter.WinStreak + 1);
				}
				else
				{
					await SetWinstreak(FirstUserToken, FirstFighter.Name, FirstFighter.WinStreak + 1);
					await SetWinstreak(SecondUserToken, SecondFighter.Name, 0);
				}
				
				SendRoosterStatus();
				SendMessageToClients("Матч Окончен");

				Storage.Sessions.Remove(Token);
			},
					 token);
		}

		private async Task SetWinstreak(string token, string roosterName, int value)
		{
			var login = await GetLoginAsync(token);
			var rooster = Storage.RoostersData[login]
								 .First(x => x.Name == roosterName);
			rooster.WinStreak = value;
		}

		private void SendMessageToClients(string message)
		{
			Task.Run(() => _firstCallback.GetBattleMessage(message));
			Task.Run(() => _secondCallback.GetBattleMessage(message));
		}

		private void SendRoosterStatus()
		{
			Task.Run(() => _firstCallback.GetRoosterStatus(FirstFighter.ToRoosterDto(), SecondFighter.ToRoosterDto()));
			Task.Run(() => _secondCallback.GetRoosterStatus(SecondFighter.ToRoosterDto(), FirstFighter.ToRoosterDto()));
		}
	}
}
