using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Предоставляет сервис проведения поединков.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Implements.BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IBattleService" />

	public class BattleService : BaseServiceWithStorage, IBattleService
	{
		#region Public Methods
		/// <summary>
		/// Производит поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>

		public void FindMatchAsync(string token, RoosterDto rooster)
		{
			var callback = OperationContext.Current.GetCallbackChannel<IBattleServiceCallback>();
			FindMatchAsync(token, rooster, callback);
		}

		public async void FindMatchAsync(string token, RoosterDto rooster, IBattleServiceCallback callback)
		{
			if (!StorageService.LoggedUsers.ContainsKey(token))
			{
				Task.Run(() => callback.FindedMatch("User was not found"));
				return;
			}

			await Task.Run(async () =>
			{
				Session session;
				if (StorageService.Sessions.Count > 0 &&
					!StorageService.Sessions.Last()
								   .Value.IsReady)
				{
					session = StorageService.Sessions.Last()
											.Value;
					session.RegisterFighter(token, rooster, callback);
				}
				else
				{
					var matchToken = await GenerateTokenAsync();
					session = new Session(matchToken);
					session.RegisterFighter(token, rooster, callback);
					StorageService.Sessions.Add(matchToken, session);
				}
			});

		}

		/// <summary>
		/// Производит отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешной отмены поиска, иначе - false.
		/// </returns>
		public bool CancelFinding(string token)
		{
			var session = StorageService.Sessions.Reverse()

								 .First(x => x.Value.RemoveFighter(token))
								 .Value;
			if (session != null)
			{
				StorageService.Sessions.Remove(session.Token);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Начинает поединк петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>

		public async void StartBattleAsync(string token, string matchToken)
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


		/// <summary>
		/// Производит сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <returns>Task.</returns>

		public async void GiveUpAsync(string token, string matchToken)
		{
			await Task.Run(async () =>
			{
				var session = StorageService.Sessions[matchToken];

				OperationContext.Current?.Channel?.Close();
				session.StopSession(true);
			});
		}
		#endregion
	}
}
