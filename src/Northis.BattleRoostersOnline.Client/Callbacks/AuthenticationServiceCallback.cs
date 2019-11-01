using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Callbacks
{
	/// <summary>
	/// Реализует контракт службы IAuthenticateServiceCallback.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateServiceCallback" />
	class AuthenticationServiceCallback : IAuthenticateServiceCallback
	{
		#region Fields
		private RoosterBrowserViewModel _roosterBrowserViewModel;

		private Logger _authServiceCallbackLogger = LogManager.GetLogger("AuthServiceCallback");
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthenticationServiceCallback"/> класса.
		/// </summary>
		/// <param name="vm">Модель-представление.</param>
		public AuthenticationServiceCallback(RoosterBrowserViewModel vm)
		{
			_roosterBrowserViewModel = vm;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Получает обновленную статистику.
		/// </summary>
		/// <param name="statistics">Статистика.</param>
		/// <param name="usersStatistics">Статистика пользователя.</param>
		public void GetNewGlobalStatistics(StatisticsDto[] statistics, UsersStatisticsDto[] usersStatistics)
		{
			_authServiceCallbackLogger.Info("Начало обновления статистики...");
			_roosterBrowserViewModel.Statistics = new StatisticsModel[statistics.Length];
			_roosterBrowserViewModel.UserStatistics = new UserStatistic[usersStatistics.Length];
			for (int i = 0; i < statistics.Length; i++)
			{
				_roosterBrowserViewModel.Statistics[i] = new StatisticsModel(statistics[i]);
			}

			for (int i = 0; i < usersStatistics.Length; i++)
			{
				_roosterBrowserViewModel.UserStatistics[i] = new UserStatistic(usersStatistics[i]);
			}
			_authServiceCallbackLogger.Info("Статистика обновлена.");
		}
		#endregion
	}
}
