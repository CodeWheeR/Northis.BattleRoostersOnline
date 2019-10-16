using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract]
	interface IEditService
	{
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
