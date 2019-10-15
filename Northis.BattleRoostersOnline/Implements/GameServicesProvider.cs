using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataTransferObjects;

namespace Northis.BattleRoostersOnline.Implements
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "GameServicesProvider" в коде и файле конфигурации.
	public class GameServicesProvider : IGameServicesProvider
	{
		public string GetData(int value)
		{
			return string.Format("You entered: {0}", value);
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			
		}
	}
}
