using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.Implements
{
	class BattleService : BaseServiceWithStorage, IBattleService
	{
		public void StartBattle(string token, string matchToken)
		{
			var callback = OperationContext.Current.GetCallbackChannel<IBattleServiceCallback>();

			var session = Storage.Sessions[matchToken];
			if (token == session.FirstUserToken)
				session.FirstCallback = callback;
			else
				session.SecondCallback = callback;

			if (session.FirstCallback != null && session.SecondCallback != null)
			{
				session.SendStartSignAsync();
				session.StartBattle();
			}
		}

		public void Beak(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void Bite(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void Pull(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void GiveUp(string token, string matchToken)
		{
			throw new NotImplementedException();
		}
	}
}
