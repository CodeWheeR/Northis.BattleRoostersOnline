using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract(CallbackContract = typeof(IBattleServiceCallback), SessionMode = SessionMode.Required)]
	public interface IBattleService
	{
		[OperationContract(IsInitiating = true)]
		Task FindMatch(string token, RoosterDto rooster);

		[OperationContract(IsTerminating = true)]
		bool CancelFinding(string token);

		[OperationContract(IsInitiating = true)]
		Task StartBattle(string token, string matchToken);
		[OperationContract]
		Task Beak(string token, string matchToken);
		[OperationContract]
		Task Bite(string token, string matchToken);
		[OperationContract]
		Task Pull(string token, string matchToken);

		[OperationContract]
		Task GiveUp(string token, string matchToken);

	}
}
