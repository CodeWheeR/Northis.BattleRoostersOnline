using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.ComponentModel;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Models
{
	class StatisticsModel
	{
		[DisplayName("Пользователь")]
		public string UserName
		{
			get;
		}
		[DisplayName("Имя петуха")]
		public string RoosterName
		{
			get;
		}
		[DisplayName("Череда побед")]
		public int WinStreak
		{
			get;
		}

		public StatisticsModel(StatisticsDto source = null)
		{
			if (source != null)
			{
				UserName = source.UserName;
				RoosterName = source.RoosterName;
				WinStreak = source.WinStreak;
			}
		}
	}
}
