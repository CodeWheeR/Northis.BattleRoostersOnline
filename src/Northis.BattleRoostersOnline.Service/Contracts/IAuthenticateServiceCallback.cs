using System.Collections.Generic;
using System.ServiceModel;

using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
	/// <summary>
	/// Контракт сервиса, ответственного за оповещения.
	/// </summary>
	public interface IAuthenticateServiceCallback
	{
		/// <summary>
		/// Контракт операции, ответственной за оповещение о начале матча.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetNewGlobalStatistics(List<StatisticsDto> statistics);
	}
}
