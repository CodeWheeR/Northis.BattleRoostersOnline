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
		private Session _session;
		private string _matchToken;

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
				if (Storage.Sessions.Count > 0 && !Storage.Sessions.Last().Value.IsStarted)
				{
					_session = Storage.Sessions.Last().Value;
					_session.RegisterFighter(token, rooster, callback);
				}
				else
				{
					_matchToken = await GenerateTokenAsync();
					_session = new Session(_matchToken);
					_session.RegisterFighter(token, rooster, callback);
					Storage.Sessions.Add(_matchToken, _session);
				}
			});
		}

		public bool CancelFinding(string token)
		{
			if (_session != null && _session.RemoveFighter(token))
			{
				Storage.Sessions.Remove(_matchToken);
				_session = null;
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
					session.SendStartSignAsync();
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

		public Task GiveUp(string token, string matchToken)
		{
			throw new NotImplementedException();
		}
	}
}
