using System.Collections.Generic;
using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт сервиса, ответственного за оповещения.
	/// </summary>
	internal interface IAuthenticateServiceCallback
	{
		/// <summary>
		/// Контракт операции, ответственной за оповещение о начале матча.
		/// </summary>
		[OperationContract]
		void GetNewGlobalStatistics(List<StatisticsDto> statistics);
	}
}
