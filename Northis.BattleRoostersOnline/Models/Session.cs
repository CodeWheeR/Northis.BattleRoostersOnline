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
	/// <summary>
	/// Класс, работающий с игровой сессией.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Implements.BaseServiceWithStorage" />
	public class Session : BaseServiceWithStorage
	{
		#region Events
		#region Private
		/// <summary>
		/// Событие начала игровой сессии.
		/// </summary>
		private event EventHandler<MatchFindedEventArgs> SessionStarted;

		/// <summary>
		/// Событие начала битвы.
		/// </summary>
		private event EventHandler BattleStarted;

		/// <summary>
		/// Событие окончания битвы.
		/// </summary>
		private event EventHandler BattleEnded;
		#endregion
		#endregion

		#region Fields
		#region Private
		/// <summary>
		/// Источник токена отмены для битвы.
		/// </summary>
		private readonly CancellationTokenSource _battleTokenSource = new CancellationTokenSource();
		/// <summary>
		/// Источник токена отмены для проверки соединения.
		/// </summary>
		private readonly CancellationTokenSource _connectionMonitorTokenSource = new CancellationTokenSource();
		#endregion
		#endregion

		#region Inner
		/// <summary>
		/// Класс, инкапсулирующий пользовательские данные.
		/// </summary>
		private class UserData
		{
			#region Fields
			#region Private
			/// <summary>
			/// Callback-сервис.
			/// </summary>
			private readonly IBattleServiceCallback _callback;
			#endregion
			#endregion

			#region .ctor
			/// <summary>
			/// Инициализирует новый объект <see cref="UserData" /> класса.
			/// </summary>
			/// <param name="callback">Callback-сервис.</param>
			public UserData(IBattleServiceCallback callback) => _callback = callback;
			#endregion

			#region Properties
			/// <summary>
			/// Возвращает или задает токен.
			/// </summary>
			/// <value>
			/// Токен.
			/// </value>
			public string Token
			{
				get;
				set;
			}

			/// <summary>
			/// Возвращает или задает петуха.
			/// </summary>
			/// <value>
			/// Петух.
			/// </value>
			public RoosterModel Rooster
			{
				get;
				set;
			}

			/// <summary>
			/// Возвращает или задает состояние готовности сессии.
			/// </summary>
			/// <value>
			/// <c>true</c> если сессия готова; иначе, <c>false</c>.
			/// </value>
			public bool IsReady
			{
				get;
				set;
			}
			#endregion

			#region Methods

			private void CarefulCallback(Action callback)
			{
				try
				{
					if (CallbackState == CommunicationState.Opened)
					{
						callback();
					}
				}
				catch (Exception e)
				{
					if (e is CommunicationException || e is TimeoutException || e is ObjectDisposedException)
					{
						(_callback as ICommunicationObject)?.Close();
					}
				}
			}

			#region Public
			/// <summary>
			/// Возвращает состояние callback.
			/// </summary>
			/// <value>
			/// Состояние callback.
			/// </value>
			public CommunicationState CallbackState => _callback is ICommunicationObject co ? co.State : CommunicationState.Closed;

			/// <summary>
			/// Асинхронно возвращает состояние петухов.
			/// </summary>
			/// <param name="yourRooster">Ваш петух.</param>
			/// <param name="enemyRooster">Петух противника.</param>
			public async Task GetRoosterStatusAsync(RoosterDto yourRooster, RoosterDto enemyRooster)
			{
				await Task.Run(() =>
				{
					CarefulCallback(() => _callback.GetRoosterStatus(yourRooster, enemyRooster));
				});
			}

			/// <summary>
			/// Асинхронно получает сообщения из битвы.
			/// </summary>
			/// <param name="message">Сообщение.</param>
			public async Task GetBattleMessageAsync(string message)
			{
				await Task.Run(() =>
				{
					CarefulCallback(() => _callback.GetBattleMessage(message));
				});
			}

			/// <summary>
			/// Асинхнронно оповещает о начале битвы.
			/// </summary>
			public async Task GetStartSignAsync()
			{
				await Task.Run(() =>
				{
					CarefulCallback(() => _callback.GetStartSign());
				});
			}

			/// <summary>
			/// Асинхнронно оповещает о нахождении матча.
			/// </summary>
			/// <param name="token">Токен.</param>
			public async Task FindedMatchAsync(string token)
			{
				await Task.Run(() =>
				{
					CarefulCallback(() => _callback.FindedMatch(token));
				});
			}

			/// <summary>
			/// Асинхнронно оповещает о завершении битвы.
			/// </summary>
			public async Task GetEndSignAsync()
			{
				await Task.Run(() =>
				{
					CarefulCallback(() => _callback.GetEndSign());
				});
			}
			#endregion
			#endregion
		}
		#endregion

		#region Properties
		#region Internal
		/// <summary>
		/// Возвращает или задает текущее состояние сессии.
		/// </summary>
		/// <value>
		/// <c>true</c> сессия запущена; иначе, <c>false</c>.
		/// </value>
		internal bool IsStarted
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает состояние готовности сессии.
		/// </summary>
		/// <value>
		/// <c>true</c> сессия готова; иначе, <c>false</c>.
		/// </value>
		internal bool IsReady
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает токен.
		/// </summary>
		/// <value>
		/// Токен.
		/// </value>
		internal string Token
		{
			get;
		}
		#endregion

		#region Private
		/// <summary>
		/// Возвращает или задает первого противника.
		/// </summary>
		/// <value>
		/// Первый противник.
		/// </value>
		private UserData FirstUser
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает второго противника.
		/// </summary>
		/// <value>
		/// Второй противник.
		/// </value>
		private UserData SecondUser
		{
			get;
			set;
		}
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Session" /> класса.
		/// </summary>
		/// <param name="token">Токен.</param>
		public Session(string token) => Token = token;
		#endregion

		#region Methods
		#region Public
		/// <summary>
		/// Регистрирует бойца.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="fighter">Боец.</param>
		/// <param name="callback">Callback сервис.</param>
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

			if (callback is ICommunicationObject co)
			{
				co.Closing += (x,y) => CheckForDeserting(token);
			}
		}

		/// <summary>
		/// Удаляет бойца.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>true - если боец успешно удален; иначе - false.</returns>
		public bool RemoveFighter(string token)
		{
			if (token == FirstUser.Token)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Вызывает событие начала сессии.
		/// </summary>
		public void SendStartSign()
		{
			IsStarted = true;
			BattleStarted?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Вызывает событие окончания сессии.
		/// </summary>
		public void SendEndSign()
		{
			IsStarted = true;
			BattleEnded?.Invoke(this, EventArgs.Empty);
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();
		}

		/// <summary>
		/// Останавливает сессию.
		/// </summary>
		/// <param name="needCheck">Необходимость проверки на дезертирство. <c>true</c> [need check].</param>
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

		/// <summary>
		/// Асинхронно начинает битву.
		/// </summary>
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

							   if (FirstUser.Rooster.Health == 0 && SecondUser.Rooster.Health == 0)
							   {
								   Task.WaitAll(SetWinstreak(FirstUser, 0), 
												SetWinstreak(SecondUser, 0));
							   }
							   else if (FirstUser.Rooster.Health == 0)
							   {
								   Task.WaitAll(SetWinstreak(FirstUser, 0), 
												SetWinstreak(SecondUser, SecondUser.Rooster.WinStreak + 1));
							   }
							   else
							   {
								   Task.WaitAll(SetWinstreak(FirstUser, FirstUser.Rooster.WinStreak + 1), 
												SetWinstreak(SecondUser, 0));
							   }

							   StorageService.Sessions.Remove(Token);
							   StorageService.SaveRoostersAsync();
							   SendEndSign();
						   },
						   token);
		}

		/// <summary>
		/// Асинхронно проверяет состояние сессии.
		/// </summary>
		public async Task ConnectionMonitor()
		{
			var token = _connectionMonitorTokenSource.Token;
			await Task.Run(async () =>
						   {
							   while (!IsStarted && !token.IsCancellationRequested)
							   {
								   await Task.Delay(1000, token);
								   SendRoosterStatus();
							   }
						   },
						   token);
		}

		/// <summary>
		/// Устанавливает готовность.
		/// </summary>
		/// <param name="token">Токен.</param>
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

		/// <summary>
		/// Проверяет готовность пользователя.
		/// </summary>
		/// <returns>true - если пользователь готов; иначе - false.</returns>
		public bool CheckForReadiness()
		{
			if (FirstUser.IsReady && SecondUser.IsReady)
			{
				return true;
			}

			return false;
		}
		#endregion

		#region Private
		/// <summary>
		/// Подписывает указанного пользователя.
		/// </summary>
		/// <param name="userData">Данные пользователя.</param>
		private void Subscribe(UserData userData)
		{
			SessionStarted += (x, y) => userData.FindedMatchAsync(y.MatchToken);
			BattleStarted += (x, y) => userData.GetStartSignAsync();
			BattleEnded += (x, y) => userData.GetEndSignAsync();
		}

		/// <summary>
		/// Устанавливает готовность.
		/// </summary>
		/// <param name="ready">Готовый пользователь.</param>
		/// <param name="maybeReady">Возможно готовый пользователь.</param>
		private void SetReady(UserData ready, UserData maybeReady)
		{
			ready.IsReady = true;
			if (!maybeReady.IsReady)
			{
				ready.GetBattleMessageAsync("Ожидайте согласия соперника");
			}
		}

		/// <summary>
		/// Асинхронно оповещает о готовности.
		/// </summary>
		private void SendReadySignAsync()
		{
			SessionStarted?.Invoke(this, new MatchFindedEventArgs(Token));
			SendRoosterStatus();
		}

		private async void CheckForDeserting(string token)
		{
			if (FirstUser != null && FirstUser.Token == token)
			{
				CheckForDeserting(FirstUser, SecondUser);
			}
			else if (SecondUser != null && SecondUser.Token == token)
			{
				CheckForDeserting(SecondUser, FirstUser);
			}
		}

		/// <summary>
		/// Проверяет опонентов на преждевременный выход из игровой сессии.
		/// </summary>
		/// <param name="deserter">Дезертир.</param>
		/// <param name="autoWinner">Технический победитель.</param>
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
					autoWinner.GetBattleMessageAsync($"Петух {deserter.Rooster.Name} бежал с поля боя");

					Task.WaitAll(SetWinstreak(deserter, 0), 
								 SetWinstreak(autoWinner, autoWinner.Rooster.WinStreak + 1));

					SendEndSign();
				}
				catch (CommunicationException e)
				{
					Debug.WriteLine(e.TargetSite + ": " + e);
				}

				StorageService.Sessions.Remove(Token);
			}
		}

		/// <summary>
		/// Асинхронно устанавливает значение серии побед.
		/// </summary>
		/// <param name="userData">Пользователь.</param>
		/// <param name="value">Значение.</param>
		private async Task SetWinstreak(UserData userData, int value)
		{
			var login = await GetLoginAsync(userData.Token);
			var rooster = StorageService.RoostersData[login]
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

		/// <summary>
		/// Производит удар с обратной связью.
		/// </summary>
		/// <param name="sender">Отправитель.</param>
		/// <param name="receiver">Получатель.</param>
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

		/// <summary>
		/// Отправляет сообщение клиентам.
		/// </summary>
		/// <param name="message">The message.</param>
		private void SendMessageToClients(string message)
		{
			FirstUser.GetBattleMessageAsync(message);
			SecondUser.GetBattleMessageAsync(message);
		}

		/// <summary>
		/// Отправляет состояние петухов клиентам.
		/// </summary>
		private void SendRoosterStatus()
		{
			FirstUser.GetRoosterStatusAsync(FirstUser.Rooster != null ? FirstUser.Rooster.ToRoosterDto() : null,
											SecondUser.Rooster != null ? SecondUser.Rooster.ToRoosterDto() : null);
			SecondUser.GetRoosterStatusAsync(SecondUser.Rooster != null ? SecondUser.Rooster.ToRoosterDto() : null,
											 FirstUser.Rooster != null ? FirstUser.Rooster.ToRoosterDto() : null);
		}
		#endregion
		#endregion
	}
}
