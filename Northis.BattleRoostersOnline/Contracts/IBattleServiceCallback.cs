using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	interface IBattleServiceCallback
	{
		[OperationContract]
		void GetRoosterStatus(RoosterDto rooster);

		[OperationContract]
		void GetBattleMessage(string message);
	}
}
