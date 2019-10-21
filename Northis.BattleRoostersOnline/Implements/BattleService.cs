using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.Implements
{
	class BattleService : BaseServiceWithStorage, IBattleService
	{

		public async Task FindMatch(string token, RoosterDto rooster)
		{
			var callback = OperationContext.Current.GetCallbackChannel<IBattleServiceCallback>();
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				Task.Run(() => callback.FindedMatch("User was not found"));
				return;
			}

			await Task.Run(async () =>
			{
				Session session;
				if (Storage.Sessions.Count > 0 && !Storage.Sessions.Last().Value.IsStarted)
				{
					session = Storage.Sessions.Last().Value;
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

		public bool CancelFinding(string token)
		{
			var session = Storage.Sessions.Reverse()
								 .First(x => x.Value.RemoveFighter(token)).Value;
			if (session != null)
			{
				Storage.Sessions.Remove(session.Token);
				return true;
			}
			return false;
		}


		public async Task StartBattle(string token, string matchToken)
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

		public Task Beak(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public Task Bite(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public Task Pull(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public async Task GiveUp(string token, string matchToken)
		{
			await Task.Run(async () =>
			{
				var session = Storage.Sessions[matchToken];
				session.StopSession(true);
			});
		}
	}
}
