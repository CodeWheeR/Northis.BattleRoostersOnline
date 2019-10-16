using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт, отвечающий за работу с петухами.
	/// </summary>
	[ServiceContract]
	public interface IEditService
	{
		/// <summary>
		/// Редактирует выбранного петуха конкретного пользователя.
		/// </summary>
		/// <param name="userID">Идентификатор.</param>
		/// <param name="roosterSeqNum">Порядковый номер петуха.</param>
		/// <param name="rooster">Петух.</param>
		[OperationContract]
		void Edit(string userID, int roosterSeqNum, RoosterDto rooster);
		[OperationContract]
		void Add(string userID, RoosterDto rooster);
		[OperationContract]
		void Remove(string userID, int roosterID);
		[OperationContract]
		void Save();
		[OperationContract]
		void Load();
	}
}
