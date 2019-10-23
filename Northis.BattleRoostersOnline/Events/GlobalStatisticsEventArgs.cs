using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Events
{
	/// <summary>
	/// Предназначен для хранения новой статистики для передачи в событие.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class GlobalStatisticsEventArgs : EventArgs
	{
		/// <summary>
		/// Возвращает или устанавливает статистику пользователей.
		/// </summary>
		public List<StatisticsDto> Statistics
		{
			get;
			set;
		} = new List<StatisticsDto>();
	}
}
