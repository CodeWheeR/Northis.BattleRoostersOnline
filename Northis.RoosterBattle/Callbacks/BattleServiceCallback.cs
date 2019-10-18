using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.RoosterBattle.GameServer;
using Northis.RoosterBattle.Models;
using Northis.RoosterBattle.ViewModels;

namespace Northis.RoosterBattle.Callbacks
{
	class BattleServiceCallback : IBattleServiceCallback
	{
		private FightViewModel _fightVm;
		public BattleServiceCallback(FightViewModel fightvm)
		{
			_fightVm = fightvm;
		}
		public void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster)
		{
			_fightVm.FirstFighter = new RoosterModel(yourRooster);
			_fightVm.SecondFighter = new RoosterModel(enemyRooster);
		}

		public void GetBattleMessage(string message)
		{
			throw new NotImplementedException();
		}

		public void GetStartSign()
		{
			throw new NotImplementedException();
		}

		public void FindedMatch(string token)
		{
			throw new NotImplementedException();
		}
	}
}
