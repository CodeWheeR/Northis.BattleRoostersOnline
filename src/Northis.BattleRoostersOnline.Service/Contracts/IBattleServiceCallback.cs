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
		/// Получает состояние петухов.
		/// </summary>
		/// <param name="yourRooster">Ваш петух.</param>
		/// <param name="enemyRooster">Петух противника.</param>
		[OperationContract(IsOneWay = true)]
		void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster);

		/// <summary>
		/// Получает сообщение с боевой сессии.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		[OperationContract(IsOneWay = true)]
		void GetBattleMessage(string message);

		/// <summary>
		/// Получает сигнал начала боя.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetStartSign();

		/// <summary>
		/// Получает информацию о подобранной сессии.
		/// </summary>
		/// <param name="token">Токен.</param>
		[OperationContract(IsOneWay = true)]
		void FindedMatch(string token);

		/// <summary>
		/// Получает сигнал об окончании матча.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetEndSign(bool isWin);
		#endregion
	}
}
