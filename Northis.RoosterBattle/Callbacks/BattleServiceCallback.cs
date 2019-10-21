using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
			if (enemyRooster != null)
				_fightVm.SecondFighter = new RoosterModel(enemyRooster);
			else
				_fightVm.SecondFighter = null;
		}

		public void GetBattleMessage(string message)
		{
			_fightVm.BattleLog += message + Environment.NewLine;
		}

		public void GetStartSign()
		{
			_fightVm.BattleStarted = true;
			_fightVm.BattleLog += "Бой начался" + Environment.NewLine;
		}

		public void FindedMatch(string token)
		{
			_fightVm.MatchToken = token;
			_fightVm.BattleLog += "Матч найден. Когда будете готовы, нажмите кнопку \"Начать бой\"" + Environment.NewLine;
		}

		public void GetEndSign()
		{
			_fightVm.BattleEnded = true;
			_fightVm.BattleLog += "Бой окончен" + Environment.NewLine;
		}
	}
}
