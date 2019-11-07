using System;
using System.Windows;
using Catel.IoC;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Properties;

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

		private readonly IMessageService _messageService;
		#endregion

		public BattleServiceCallback()
		{
			var container = this.GetServiceLocator();
			_messageService = container.ResolveType<IMessageService>();
		}

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

			_battleServiceCallbackLogger.Info(Resources.StrInfoStatusUpdated);
		}

		/// <summary>
		/// Получает игровое сообщение и выводит в игровую консоль.
		/// </summary>
		/// <param name="message">The message.</param>
		public void GetBattleMessage(string message)
		{
			_fightVm.BattleLog += message + Environment.NewLine;
			_battleServiceCallbackLogger.Info(Resources.StrInfoReciveMessage, message);
		}

		/// <summary>
		/// Получает сигнал к началу сражения.
		/// </summary>
		public void GetStartSign()
		{
			_fightVm.BattleStarted = true;
			_fightVm.BattleLog += "Бой начался" + Environment.NewLine;
			_battleServiceCallbackLogger.Info(Resources.StrInfoStartFighting);
		}

		/// <summary>
		/// Получает токен найденного матча.
		/// </summary>
		/// <param name="token">The token.</param>
		public void FindedMatch(string token)
		{
			if (token == "User was not found")
			{
				_messageService.ShowAsync("Попытка поиска матча не авторизованным пользователем", "Предупреждение");
				_battleServiceCallbackLogger.Error(Resources.StrErrorNotLoginedUserFind);
				return;
			}
			if (token == "Rooster was not found")
			{
				_messageService.ShowAsync("Попытка поиска матча не авторизованным петухом", "Предупреждение");
				_battleServiceCallbackLogger.Error(Resources.StrErrorNotLoginedRoosterFind, "Предупреждение");
				return;
			}
			if (token == "SameLogin")
			{
				_messageService.ShowAsync("Попытка начать бой друг с другом");
				_battleServiceCallbackLogger.Error(Resources.StrErrorFightAgainstYourself);
				return;
			}
			_fightVm.BattleEnded = false;
			_fightVm.IsFinding = false;
			_fightVm.MatchToken = token;
			_fightVm.BattleLog += "Матч найден. Когда будете готовы, нажмите кнопку \"Начать бой\"" + Environment.NewLine;
			_battleServiceCallbackLogger.Info(Resources.StrInfoPrepareToFight);
		}

		/// <summary>
		/// Получает сигнал о завершении боя.
		/// </summary>
		public void GetEndSign()
		{
			_fightVm.BattleEnded = true;
			_fightVm.BattleLog += "Бой окончен" + Environment.NewLine;
			_battleServiceCallbackLogger.Info(Resources.StrInfoFightEnded);
		}
		#endregion
	}
}
