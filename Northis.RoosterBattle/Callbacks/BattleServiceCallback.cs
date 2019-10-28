using System;
using Northis.RoosterBattle.GameServer;
using Northis.RoosterBattle.Models;
using Northis.RoosterBattle.ViewModels;

namespace Northis.RoosterBattle.Callbacks
{
	/// <summary>
	/// Реализует интерфейс callback'ов боевого сервиса.
	/// </summary>
	/// <seealso cref="Northis.RoosterBattle.GameServer.IBattleServiceCallback" />
	internal class BattleServiceCallback : IBattleServiceCallback
	{
		#region Fields
		private readonly FightViewModel _fightVm;
		#endregion

		#region Public Methods		
		/// <summary>
		/// Инициализует новый объект класса <see cref="BattleServiceCallback" />.
		/// </summary>
		/// <param name="fightvm">The fightvm.</param>
		public BattleServiceCallback(FightViewModel fightvm) => _fightVm = fightvm;

		/// <summary>
		/// Получает состояние петухов в бою.
		/// </summary>
		/// <param name="yourRooster">Свой петух.</param>
		/// <param name="enemyRooster">Петух врага.</param>
		public void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster)
		{
			var rooster = new RoosterModel(yourRooster);
			_fightVm.FirstFighter = rooster;
			if (enemyRooster != null)
			{
				_fightVm.SecondFighter = new RoosterModel(enemyRooster);
			}
			else
			{
				_fightVm.SecondFighter = null;
			}
		}

		/// <summary>
		/// Получает игровое сообщение и выводит в игровую консоль.
		/// </summary>
		/// <param name="message">The message.</param>
		public void GetBattleMessage(string message)
		{
			_fightVm.BattleLog += message + Environment.NewLine;
		}

		/// <summary>
		/// Получает сигнал к началу сражения.
		/// </summary>
		public void GetStartSign()
		{
			_fightVm.BattleStarted = true;
			_fightVm.BattleLog += "Бой начался" + Environment.NewLine;
		}

		/// <summary>
		/// Получает токен найденного матча.
		/// </summary>
		/// <param name="token">The token.</param>
		public void FindedMatch(string token)
		{
			_fightVm.BattleEnded = false;
			_fightVm.IsFinding = false;
			_fightVm.MatchToken = token;
			_fightVm.BattleLog += "Матч найден. Когда будете готовы, нажмите кнопку \"Начать бой\"" + Environment.NewLine;
		}

		/// <summary>
		/// Получает сигнал о завершении боя.
		/// </summary>
		public void GetEndSign()
		{
			_fightVm.BattleEnded = true;
			_fightVm.BattleLog += "Бой окончен" + Environment.NewLine;
		}
		#endregion
	}
}
