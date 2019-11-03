using System;
using System.Collections.Generic;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Events
{
	/// <summary>
	/// Предназначен для хранения информации о статистике. Передается в обработчик события.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class GlobalStatisticsEventArgs : EventArgs
	{
        #region Properties
        /// <summary>
        /// Возвращает или устанавливает статистику пользователей.
        /// </summary>
        public List<StatisticsDto> Statistics
		{
			get;
			set;
		} = new List<StatisticsDto>();
        #endregion
    }
}
