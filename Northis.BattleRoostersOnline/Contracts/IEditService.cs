using System.ServiceModel;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract]
	interface IEditService
	{
		[OperationContract]
		void Edit(int userID, int roosterID, RoosterDto rooster);
		[OperationContract]
		void Add(int userID, RoosterDto rooster);
		[OperationContract]
		void Remove(int userID, int roosterID);
	}
}
