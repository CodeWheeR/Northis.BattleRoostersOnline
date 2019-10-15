using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Northis.BattleRoostersOnline;

namespace Northis.BattleRoostersOnline.Contracts
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IGameServicesProvider" в коде и файле конфигурации.
	[ServiceContract]
	public interface IGameServicesProvider
	{
		[OperationContract]
		string GetData(int value);


		// TODO: Добавьте здесь операции служб
	}
}
