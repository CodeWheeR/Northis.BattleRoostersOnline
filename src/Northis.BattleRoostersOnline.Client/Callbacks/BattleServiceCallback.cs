using System;
using System.Windows.Media;
using AutoMapper;
using Catel.IoC;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Extensions;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.Properties;
using Northis.BattleRoostersOnline.Client.ViewModels;
using BattleStatus = Northis.BattleRoostersOnline.Client.GameServer.BattleStatus;

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
		private readonly Logger _battleServiceCallbackLogger = LogManager.GetLogger("FightServiceCallback");
		#endregion

		#region Public Methods		
		/// <summary>
		/// Инициализует новый объект класса <see cref="BattleServiceCallback" />.
		/// </summary>
		/// <param name="fightvm">Объект модели представления FightWindow.</param>
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
		/// <param name="message">Сообщение.</param>
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
		/// <param name="token">Токен.</param>
		public void FindedMatch(string token)
		{
			var container = this.GetServiceLocator();
			var messageService = container.ResolveType<IMessageService>();
			var mapper = container.ResolveType<IMapper>();

			if (Enum.TryParse(token, out BattleStatus serverResult))
			{
				var result = mapper.Map<BattleStatus, Models.BattleStatus>(serverResult);
				messageService.ShowAsync(result.GetDisplayFromResource(), "Предупреждение");
				_battleServiceCallbackLogger.Error(result.GetDisplayFromResource());
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
		public void GetEndSign(bool isWinner)
		{
			_fightVm.BattleEnded = true;
			_fightVm.BattleLog += "Бой окончен" + Environment.NewLine;

			_battleServiceCallbackLogger.Info(Resources.StrInfoFightEnded);

			if (!isWinner)
			{
				_fightVm.LeftRoosterBorderColor = Brushes.Red;
				_fightVm.RightRoosterBorderColor = Brushes.LimeGreen;
			}
			else
			{
				_fightVm.RightRoosterBorderColor = Brushes.Red;
				_fightVm.LeftRoosterBorderColor = Brushes.LimeGreen;
			}
		}
		#endregion
	}
}
