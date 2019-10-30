using System;
using System.Collections.Generic;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Events
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
