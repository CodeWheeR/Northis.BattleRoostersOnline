using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Events;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.Models
{
	public class Session : BaseServiceWithStorage
	{
		private event EventHandler<MatchFindedEventArgs> SessionStarted;
		private event EventHandler BattleStarted;
		private event EventHandler BattleEnded;

		private CancellationTokenSource _battleTokenSource = new CancellationTokenSource();
		private readonly CancellationTokenSource _connectionMonitorTokenSource = new CancellationTokenSource();

		private class UserData
		{
			private IBattleServiceCallback _callback;

			public UserData(IBattleServiceCallback callback)
			{
				_callback = callback;
			}

			public string Token
			{
				get;
				set;
			}

			public RoosterModel Rooster
			{
				get;
				set;
			}

			public bool IsReady
			{
				get;
				set;
			}

			public CommunicationState CallbackState => ((ICommunicationObject) _callback).State;

			public async Task GetRoosterStatusAsync(RoosterDto yourRooster, RoosterDto enemyRooster)
			{
				try
				{
					await Task.Run(() => _callback.GetRoosterStatus(yourRooster, enemyRooster));
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}
			}

			public async Task GetBattleMessageAsync(string message)
			{
				try
				{
					await Task.Run(() => _callback.GetBattleMessage(message));
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}
			}

			public async Task GetStartSignAsync()
			{
				try
				{
					await Task.Run(() => _callback.GetStartSign());
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}
			}

			public async Task FindedMatchAsync(string token)
			{
				try
				{
					await Task.Run(() => _callback.FindedMatch(token));
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}
			}

			public async Task GetEndSignAsync()
			{
				try
				{
					await Task.Run(() => _callback.GetEndSign());
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}
			}
		}

		internal bool IsStarted
		{
			get;
			set;
		}

		internal bool IsReady
		{
			get;
			set;
		}

		private UserData FirstUser
		{
			get;
			set;
		}

		private UserData SecondUser
		{
			get;
			set;
		}

		internal string Token
		{
			get;
		}

		public Session(string token) => Token = token;

		public void RegisterFighter(string token, RoosterDto fighter, IBattleServiceCallback callback)
		{
			if (FirstUser == null)
			{
				FirstUser = new UserData(callback)
				{
					Rooster = new RoosterModel(fighter),
					Token = token,
					IsReady = false
				};
				Subscribe(FirstUser);
			}
			else if (SecondUser == null)
			{
				IsReady = true;
				SecondUser = new UserData(callback)
				{
					Rooster = new RoosterModel(fighter),
					Token = token,
					IsReady = false
				};
				Subscribe(SecondUser);
				SendReadySignAsync();
				ConnectionMonitor();
			}
		}

		private void Subscribe(UserData userData)
		{
			SessionStarted += (x, y) => userData.FindedMatchAsync(y.MatchToken);
			BattleStarted += (x, y) => userData.GetStartSignAsync();
			BattleEnded += (x, y) => userData.GetEndSignAsync();
		}

		public void SetReady(string token)
		{
			if (token == FirstUser.Token)
			{
				SetReady(FirstUser, SecondUser);
			}
			else
			{
				SetReady(SecondUser, FirstUser);
			}
		}

		private void SetReady(UserData ready, UserData maybeReady)
		{
			ready.IsReady = true;
			if (!maybeReady.IsReady)
			{
				ready.GetBattleMessageAsync("Ожидайте согласия соперника");
			}
		}

		public bool CheckForReadiness()
		{
			if (FirstUser.IsReady && SecondUser.IsReady)
			{
				return true;
			}

			return false;
		}

		private void SendReadySignAsync()
		{
			SessionStarted?.Invoke(this, new MatchFindedEventArgs(Token));
			SendRoosterStatus();
		}

		public bool RemoveFighter(string token)
		{
			if (token == FirstUser.Token)
			{
				return true;
			}

			return false;
		}

		public void SendStartSign()
		{
			IsStarted = true;
			BattleStarted?.Invoke(this, EventArgs.Empty);
		}

		public void SendEndSign()
		{
			IsStarted = true;
			BattleEnded?.Invoke(this, EventArgs.Empty);
		}

		public async Task ConnectionMonitor()
		{
			var token = _connectionMonitorTokenSource.Token;
			await Task.Run(async () =>
						   {
							   while (!token.IsCancellationRequested)
							   {
								   await Task.Delay(1000);
								   if (!IsStarted)
								   {
									   try
									   {
										   SendRoosterStatus();
									   }
									   catch (CommunicationException e)
									   {
										   Debug.WriteLine(e);
									   }
								   }
								   CheckForDeserting(FirstUser, SecondUser);
								   CheckForDeserting(SecondUser, FirstUser);
							   }
						   },
						   token);
		}

		private async void CheckForDeserting(UserData deserter, UserData autoWinner)
		{
			if (deserter.CallbackState != CommunicationState.Opened)
			{
				if (!_battleTokenSource.IsCancellationRequested)
				{
					StopSession();
				}
				try
				{
					SecondUser.GetBattleMessageAsync($"Петух {deserter.Rooster.Name} бежал с поля боя");
					await SetWinstreak(deserter, 0);
					await SetWinstreak(autoWinner, autoWinner.Rooster.WinStreak + 1);
					SendEndSign();
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e);
				}

				Storage.Sessions.Remove(Token);
			}
		}

		public void StopSession(bool needCheck = false)
		{
			_battleTokenSource.Cancel();
			_connectionMonitorTokenSource.Cancel();
			if (needCheck)
			{
				CheckForDeserting(FirstUser, SecondUser);
				CheckForDeserting(SecondUser, FirstUser);
			}
		}

		public async Task StartBattle()
		{
			var token = _battleTokenSource.Token;

			await Task.Run(async () =>
						   {
							   while (FirstUser.Rooster.Health > 0 && SecondUser.Rooster.Health > 0 && !token.IsCancellationRequested)
							   {
								   MakeHitWithFeedback(FirstUser.Rooster, SecondUser.Rooster);
								   SendRoosterStatus();

								   if (SecondUser.Rooster.Health == 0)
								   {
									   break;
								   }

								   await Task.Delay(300, token);

								   MakeHitWithFeedback(SecondUser.Rooster, FirstUser.Rooster);
								   SendRoosterStatus();

								   if (FirstUser.Rooster.Health == 0)
								   {
									   break;
								   }

								   await Task.Delay(300, token);
							   }

							   SendEndSign();

							   if (FirstUser.Rooster.Health == 0 && SecondUser.Rooster.Health == 0)
							   {
								   await SetWinstreak(FirstUser, 0);
								   await SetWinstreak(SecondUser, 0);
							   }
							   else if (FirstUser.Rooster.Health == 0)
							   {
								   await SetWinstreak(FirstUser, 0);
								   await SetWinstreak(SecondUser, SecondUser.Rooster.WinStreak + 1);
							   }
							   else
							   {
								   await SetWinstreak(FirstUser, FirstUser.Rooster.WinStreak + 1);
								   await SetWinstreak(SecondUser, 0);
							   }
							   


							   Storage.Sessions.Remove(Token);
						   },
						   token);
		}

		private async Task SetWinstreak(UserData userData, int value)
		{
			var login = await GetLoginAsync(userData.Token);
			var rooster = Storage.RoostersData[login]
								 .First(x => x.Name == userData.Rooster.Name);
			rooster.WinStreak = value;
			if (value != 0)
			{
				userData.GetBattleMessageAsync("Вы победили");
			}
			else
			{
				userData.GetBattleMessageAsync("Вы проиграли");
			}

		}

		private void MakeHitWithFeedback(RoosterModel sender, RoosterModel receiver)
		{
			var damage = receiver.TakeHit(sender);
			if (damage > 0)
			{
				SendMessageToClients($"Петух {sender.Name} нанес {damage} ед. урона петуху {receiver.Name}");
			}
			else
			{
				SendMessageToClients($"Петух {receiver.Name} уклонился от удара петуха {sender.Name}");
			}
		}

		private void SendMessageToClients(string message)
		{
			FirstUser.GetBattleMessageAsync(message);
			SecondUser.GetBattleMessageAsync(message);
		}

		private void SendRoosterStatus()
		{
			FirstUser.GetRoosterStatusAsync(FirstUser.Rooster.ToRoosterDto(), SecondUser.Rooster.ToRoosterDto());
			SecondUser.GetRoosterStatusAsync(SecondUser.Rooster.ToRoosterDto(), FirstUser.Rooster.ToRoosterDto());
		}
	}
}
