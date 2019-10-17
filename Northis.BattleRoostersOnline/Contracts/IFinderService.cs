using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract]
	interface IFinderService
	{
		[OperationContract]
		void FindMatch(string token);

		[OperationContract]
		void CancelFinding(string token);
	}
}
