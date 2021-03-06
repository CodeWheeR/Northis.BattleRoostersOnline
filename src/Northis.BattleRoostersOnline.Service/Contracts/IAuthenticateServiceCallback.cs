﻿using System.Collections.Generic;
using System.ServiceModel;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
	/// <summary>
	/// Оповещает пользователей о состоянии аутентификации.
	/// </summary>
	public interface IAuthenticateServiceCallback
	{
		#region Operation Contracts
		/// <summary>
		/// Оповещает о начале матча.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetNewGlobalStatistics(List<StatisticsDto> statistics, List<UsersStatisticsDto> usersStatistics);

		/// <summary>
		/// Оповещает об остановке сервера.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetServerStopMessage(string message);
		#endregion
	}
}
