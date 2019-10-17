using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Contracts;

namespace Northis.BattleRoostersOnline.Implements
{
	class BattleService : BaseServiceWithStorage, IBattleService
	{
		public void StartBattle(string token, string matchToken, IBattleServiceCallback callback)
		{
			throw new NotImplementedException();
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
