using System.ServiceModel;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
    /// <summary>
    /// Оповещает клиентов о состоянии боя.
    /// </summary>
    public interface IBattleServiceCallback
	{
        #region Operation Contracts
        /// <summary>
        /// Контракт операции. Отвечает за предоставление статуса петуха.
        /// </summary>
        /// <param name="yourRooster">Ваш петух.</param>
        /// <param name="enemyRooster">Петух противника.</param>
        [OperationContract(IsOneWay = true)]
		void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster);

		/// <summary>
		/// Контракт операции. Отвечает за предрставление информации о битве.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		[OperationContract(IsOneWay = true)]
		void GetBattleMessage(string message);

		/// <summary>
		/// Контракт операции. Отвечает за предоставление информации о начале боя.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetStartSign();

		/// <summary>
		/// Контракт операции. Отвечает за оповещение о нахождении матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		[OperationContract(IsOneWay = true)]
		void FindedMatch(string token);

		/// <summary>
		/// Контракт операции. Отвечает за оповещение об окончании матча.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetEndSign();
        #endregion
    }
}
