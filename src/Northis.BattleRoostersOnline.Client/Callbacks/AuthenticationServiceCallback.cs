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

		public void GetNewGlobalStatistics(StatisticsDto[] statistics)
		{
			_authServiceCallbackLogger.Info("Начало обновления статистики...");
			_roosterBrowserViewModel.Statistics = new StatisticsModel[statistics.Length];
			for (int i = 0; i < statistics.Length; i++)
			{
				_roosterBrowserViewModel.Statistics[i] = new StatisticsModel(statistics[i]);
			}
			_authServiceCallbackLogger.Info("Статистика обновлена.");
		}
	}
}
