using System.Collections.Generic;
using System.ServiceModel;

using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
    /// <summary>
    /// Оповещает пользователей о состоянии аунтефикации.
    /// </summary>
    public interface IAuthenticateServiceCallback
	{
        #region Operation Contracts
        /// <summary>
        /// Контракт операции. Оповещает о начале матча.
        /// </summary>
        [OperationContract(IsOneWay = true)]
		void GetNewGlobalStatistics(List<StatisticsDto> statistics, List<UsersStatisticsDto> usersStatistics);
        #endregion
    }
}
