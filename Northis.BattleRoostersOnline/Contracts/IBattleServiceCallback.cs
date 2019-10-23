using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт сервиса Callbacks, оповещающего клиентов.
	/// </summary>
	public interface IBattleServiceCallback
	{
		/// <summary>
		/// Контракт операции, отвечающий за предоставление статуса петуха.
		/// </summary>
		/// <param name="yourRooster">Ваш петух.</param>
		/// <param name="enemyRooster">Петух противника.</param>
		[OperationContract]
		void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster);

		/// <summary>
		/// Контракт операции, ответственный за предрставление информации о битве.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		[OperationContract]
		void GetBattleMessage(string message);

		/// <summary>
		/// Контракт операции, ответственный за предоставление информации о начале боя.
		/// </summary>
		[OperationContract]
		void GetStartSign();

		/// <summary>
		/// Контракт операции, ответственный за оповещение о нахождении матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		[OperationContract]
		void FindedMatch(string token);

		/// <summary>
		/// Контракт операции, ответственный за оповещение об окончании матча.
		/// </summary>
		[OperationContract]
		void GetEndSign();
	}
}
