using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Events
{
	public class GlobalStatisticsEventArgs : EventArgs
	{
		public List<StatisticsDto> Statistics
		{
			get;
			set;
		} = new List<StatisticsDto>();
	}
}
