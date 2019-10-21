using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	public interface IBattleServiceCallback
	{
		[OperationContract]
		void GetRoosterStatus(RoosterDto yourRooster, RoosterDto enemyRooster);

		[OperationContract]
		void GetBattleMessage(string message);

		[OperationContract]
		void GetStartSign();

		[OperationContract]
		void FindedMatch(string token);

		[OperationContract]
		void GetEndSign();
	}
}
