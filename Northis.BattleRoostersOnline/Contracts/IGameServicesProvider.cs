using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Northis.BattleRoostersOnline.DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract]
	public interface IGameServicesProvider
	{
		[OperationContract]
		string GetData(int value);


		// TODO: Добавьте здесь операции служб
	}
}
