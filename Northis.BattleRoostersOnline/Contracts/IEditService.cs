using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт, отвечающий за работу с петухами.
	/// </summary>
	[ServiceContract]
	interface IEditService
	{
		/// <summary>
		/// Редактирует выбранного петуха конкретного пользователя.
		/// </summary>
		/// <param name="userID">Идентификатор .</param>
		/// <param name="roosterID">The rooster identifier.</param>
		/// <param name="rooster">The rooster.</param>
		[OperationContract]
		void Edit(string userID, int roosterID, RoosterDto rooster);
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
