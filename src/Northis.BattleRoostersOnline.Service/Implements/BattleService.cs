using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Предоставляет сервис проведения поединков.
	/// </summary>
	/// <seealso cref="BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IBattleService" />

	public class BattleService : BaseServiceWithStorage, IBattleService
	{
        #region Fields
        private Logger _logger = LogManager.GetCurrentClassLogger();
		#endregion

		#region ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="BattleService"/> класса.
		/// </summary>
		/// <param name="storage">Объект хранилища. </param>
		public BattleService(IDataStorageService storage) : base(storage)
		{

		}
		#endregion

        #region Public Methods
        /// <summary>
        /// Производит поиск матча.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="rooster">Петух.</param>
        public void FindMatchAsync(string token, string roosterToken)
		{
			var callback = OperationContext.Current.GetCallbackChannel<IBattleServiceCallback>();
			FindMatchAsync(token, roosterToken, callback);
		}
        /// <summary>
        /// Асинхронно производит поиск матча.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="roosterToken">Токен петуха.</param>
        /// <param name="callback">Метод оповещения пользователя.</param>
        public async void FindMatchAsync(string token, string roosterToken, IBattleServiceCallback callback)
		{
			if (!StorageService.LoggedUsers.ContainsKey(token))
			{
				Task.Run(() => callback.FindedMatch("User was not found"));
				_logger.Warn($"Попытка поиска матча не авторизованным пользователем {token}");
				return;
			}
			var login = await GetLoginAsync(token);

			if (!StorageService.RoostersData.ContainsKey(login) || !StorageService.RoostersData[login]
									.ContainsKey(roosterToken))
			{
				Task.Run(() => callback.FindedMatch("Rooster was not found"));
				_logger.Warn($"Попытка поиска матча не созданным петухом {roosterToken}");
				return;
			}

			try
			{
				await Task.Run(async () =>
				{
					Session session;
					if (StorageService.Sessions.Count > 0 &&
						!StorageService.Sessions.Last()
									   .Value.IsReady)
					{
						session = StorageService.Sessions.Last()
												.Value;
						session.RegisterFighter(token, StorageService.RoostersData[login][roosterToken], callback);
					}
					else
					{
						var matchToken = await GenerateTokenAsync(StorageService.Sessions.ContainsKey);
						session = new Session(matchToken, StorageService);
						session.RegisterFighter(token, StorageService.RoostersData[login][roosterToken], callback);
						StorageService.Sessions.Add(matchToken, session);
					}
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw;
			}

		}

		/// <summary>
		/// Производит отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешной отмены поиска, иначе - false.
		/// </returns>
		public async Task<bool> CancelFinding(string token)
		{
			try
			{
				return await Task.Run<bool>(() =>
				{
					if (StorageService.Sessions.Count > 0)
					{
						var session = StorageService.Sessions.Reverse()
													.First(x => x.Value.RemoveFighter(token))
													.Value;
						if (session != null)
						{
							StorageService.Sessions.Remove(session.Token);
							_logger.Info($"Поиск матча был отменен пользователем {token}, сессия {session.Token} закрыта");
							return true;
						}

						return false;
					}
					return true;
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw;
			}
			
		}

		/// <summary>
		/// Асинхронно начинает поединк петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public async void StartBattleAsync(string token, string matchToken)
		{
			try
			{
				await Task.Run(async () =>
				{
					var session = StorageService.Sessions[matchToken];
					session.SetReady(token);

					if (session.CheckForReadiness())
					{
						session.SendStartSign();
						await session.StartBattle();
					}
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw;
			}
		}


		/// <summary>
		/// Асинхронно производит сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public async void GiveUpAsync(string token, string matchToken)
		{
			try
			{
				await Task.Run(async () =>
				{
					var session = StorageService.Sessions[matchToken];

					OperationContext.Current?.Channel?.Close();
					session.StopSession(true);
				});
				var login = await GetLoginAsync(token);
				_logger.Info($"Пользователь {(login == "" ? token : login)} дезертировал");
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw;
			}
		}
		#endregion
	}
}
