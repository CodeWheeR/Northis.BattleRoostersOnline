using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Callbacks
{
	class AuthenticationServiceCallback : IAuthenticateServiceCallback
	{
		private RoosterBrowserViewModel _roosterBrowserViewModel;

		private Logger _authServiceCallbackLogger = LogManager.GetLogger("AuthServiceCallback");

		public AuthenticationServiceCallback(RoosterBrowserViewModel vm)
		{
			_roosterBrowserViewModel = vm;
		}

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
	}
}
