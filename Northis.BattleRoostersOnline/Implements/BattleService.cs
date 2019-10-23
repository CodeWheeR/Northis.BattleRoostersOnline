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
		#region Methods
		#region Public
		/// <summary>
		/// Производит поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>

		public async Task FindMatchAsync(string token, RoosterDto rooster)
		{
			var callback = OperationContext.Current.GetCallbackChannel<IBattleServiceCallback>();
			await FindMatchAsync(token, rooster, callback);
		}

		public async Task FindMatchAsync(string token, RoosterDto rooster, IBattleServiceCallback callback)
		{
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				Task.Run(() => callback.FindedMatch("User was not found"));
				return;
			}

			await Task.Run(async () =>
			{
				Session session;
				if (Storage.Sessions.Count > 0 &&
					!Storage.Sessions.Last()
							.Value.IsStarted)
				{
					session = Storage.Sessions.Last()
									 .Value;
					session.RegisterFighter(token, rooster, callback);
				}
				else
				{
					var matchToken = await GenerateTokenAsync();
					session = new Session(matchToken);
					session.RegisterFighter(token, rooster, callback);
					Storage.Sessions.Add(matchToken, session);
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
			var session = Storage.Sessions.Reverse()

								 .First(x => x.Value.RemoveFighter(token))
								 .Value;
			if (session != null)
			{
				Storage.Sessions.Remove(session.Token);
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

		public async Task StartBattleAsync(string token, string matchToken)
		{
			await Task.Run(async () =>
			{
				var session = Storage.Sessions[matchToken];
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

		public async Task GiveUpAsync(string token, string matchToken)
		{
			await Task.Run(async () =>
			{
				var session = Storage.Sessions[matchToken];

				OperationContext.Current?.Channel?.Close();
				session.StopSession(true);
			});
		}
		#endregion
		#endregion
	}
}
