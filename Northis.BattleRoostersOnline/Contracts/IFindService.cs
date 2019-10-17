using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IFindServiceCallback))]
	interface IFindService
	{
		[OperationContract]
		void FindMatch(string token, RoosterDto rooster);

		[OperationContract]
		bool CancelFinding(string token);
	}
}
