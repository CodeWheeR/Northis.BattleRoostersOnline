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
	class AuthenticationServiceCallback : IAuthenticateServiceCallback
	{
		private RoosterBrowserViewModel _roosterBrowserViewModel;

		public AuthenticationServiceCallback(RoosterBrowserViewModel vm)
		{
			_roosterBrowserViewModel = vm;
		}

		public void GetNewGlobalStatistics(StatisticsDto[] statistics)
		{
			_roosterBrowserViewModel.Statistics = new StatisticsModel[statistics.Length];
			for (int i = 0; i < statistics.Length; i++)
			{
				_roosterBrowserViewModel.Statistics[i] = new StatisticsModel(statistics[i]);
			}
		}
	}
}
