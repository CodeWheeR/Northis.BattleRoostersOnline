using System;
using System.Windows;
using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Callbacks
{
	/// <summary>
	/// Реализует интерфейс callback'ов боевого сервиса.
	/// </summary>
	/// <seealso cref="IBattleServiceCallback" />
	internal class BattleServiceCallback : IBattleServiceCallback
	{
		#region Fields
		private readonly FightViewModel _fightVm;

		private Logger _battleServiceCallbackLogger = LogManager.GetLogger("BattleServiceCallback");

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

			_battleServiceCallbackLogger.Info("Статус готовности петухов к сражению обновлен.");
		}

		/// <summary>
		/// Получает игровое сообщение и выводит в игровую консоль.
		/// </summary>
		/// <param name="message">The message.</param>
		public void GetBattleMessage(string message)
		{
			_fightVm.BattleLog += message + Environment.NewLine;
			_battleServiceCallbackLogger.Info($"Получено сообщение: {message}.");
		}

		/// <summary>
		/// Получает сигнал к началу сражения.
		/// </summary>
		public void GetStartSign()
		{
			_fightVm.BattleStarted = true;
			_fightVm.BattleLog += "Бой начался" + Environment.NewLine;
			_battleServiceCallbackLogger.Info($"Начало боя: .");
		}

		/// <summary>
		/// Получает токен найденного матча.
		/// </summary>
		/// <param name="token">The token.</param>
		public void FindedMatch(string token)
		{
			if (token == "User was not found")
			{
				MessageBox.Show("Попытка поиска матча не авторизованным пользователем");
				_battleServiceCallbackLogger.Error("ППопытка поиска матча не авторизованным пользователем.");
				return;
			}
			_fightVm.BattleEnded = false;
			_fightVm.IsFinding = false;
			_fightVm.MatchToken = token;
			_fightVm.BattleLog += "Матч найден. Когда будете готовы, нажмите кнопку \"Начать бой\"" + Environment.NewLine;
			_battleServiceCallbackLogger.Info("Получено сообщение: Матч найден. Когда будете готовы, нажмите кнопку \"Начать бой\".");
		}

		/// <summary>
		/// Получает сигнал о завершении боя.
		/// </summary>
		public void GetEndSign()
		{
			_fightVm.BattleEnded = true;
			_fightVm.BattleLog += "Бой окончен" + Environment.NewLine;
			_battleServiceCallbackLogger.Info("Получено сообщение: Бой окончен.");
		}
		#endregion
	}
}
