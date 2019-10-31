using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Models
{
	class UserStatistic
	{
		[DisplayName("В сети")]
		public bool IsOnline
		{
			get;
		}

		[DisplayName("Пользователь")]
		public string UserName
		{
			get;
		}
		[DisplayName("Количество побед")]
		public int UserScore
		{
			get;
		}

		public UserStatistic(UsersStatisticsDto source)
		{
			UserName = source.UserName;
			UserScore = source.UserScore;
			IsOnline = source.IsOnline;
		}
	}
}
