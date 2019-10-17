using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;

namespace Northis.BattleRoostersOnline.Implements
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public class FindService : BaseServiceWithStorage, IFindService
	{
		private Session _session;
		private string _matchToken;

		public async void FindMatch(string token, RoosterDto rooster)
		{ 
			var callback = OperationContext.Current.GetCallbackChannel<IFindServiceCallback>();
			if (!Storage.LoggedUsers.ContainsKey(token))
			{
				Task.Run(() => callback.FindedMatch("User was not found"));
				return;
			}

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
		}

		public bool CancelFinding(string token)
		{
			if (_session != null && _session.RemoveFighter(token))
			{
				Storage.Sessions.Remove(_matchToken);
				return true;
			}
			return false;
		}

	}
}
