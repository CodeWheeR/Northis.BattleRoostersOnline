using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.ComponentModel;
using Northis.BattleRoostersOnline.GameClient.GameServer;

namespace Northis.BattleRoostersOnline.GameClient.Models
{
	class StatisticsModel
	{
		[DisplayName("Пользователь")]
		public string UserName
		{
			get;
			set;
		}
		[DisplayName("Имя петуха")]
		public string RoosterName
		{
			get;
			set;
		}
		[DisplayName("Череда побед")]
		public int WinStreak
		{
			get;
			set;
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
