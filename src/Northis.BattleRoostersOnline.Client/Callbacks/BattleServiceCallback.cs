using System;
using System.Windows;
using AutoMapper;
using Catel.IoC;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Extensions;
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

		private readonly IMapper _mapper;
		#endregion

		public BattleServiceCallback()
		{
			var container = this.GetServiceLocator();
			_messageService = container.ResolveType<IMessageService>();
			_mapper = container.ResolveType<IMapper>();
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

			if (!Enum.TryParse(token, out GameServer.BattleStatus serverResult))
			{
				var result = _mapper.Map<GameServer.BattleStatus, Models.BattleStatus>(serverResult);
				_messageService.ShowAsync(result.GetDisplayFromResource(), "Предупреждение");
				_battleServiceCallbackLogger.Error(result.GetDisplayFromResource());
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
