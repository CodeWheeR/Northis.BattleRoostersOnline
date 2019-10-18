using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract(CallbackContract = typeof(IBattleServiceCallback), SessionMode = SessionMode.Required)]
	interface IBattleService
	{
		[OperationContract(IsInitiating = true)]
		void StartBattle(string token, string matchToken);
		[OperationContract]
		void Beak(string token, string matchToken);
		[OperationContract]
		void Bite(string token, string matchToken);
		[OperationContract]
		void Pull(string token, string matchToken);

		[OperationContract]
		void GiveUp(string token, string matchToken);

	}
}
