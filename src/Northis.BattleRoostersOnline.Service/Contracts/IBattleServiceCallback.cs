using System.ServiceModel;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
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
		[OperationContract(IsOneWay = true)]
		void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster);

		/// <summary>
		/// Контракт операции, ответственный за предрставление информации о битве.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		[OperationContract(IsOneWay = true)]
		void GetBattleMessage(string message);

		/// <summary>
		/// Контракт операции, ответственный за предоставление информации о начале боя.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetStartSign();

		/// <summary>
		/// Контракт операции, ответственный за оповещение о нахождении матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		[OperationContract(IsOneWay = true)]
		void FindedMatch(string token);

		/// <summary>
		/// Контракт операции, ответственный за оповещение об окончании матча.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void GetEndSign();
	}
}
