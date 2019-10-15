using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CommonServiceLocator;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "GameServicesProvider" в коде и файле конфигурации.
	public class GameServicesProvider : IGameServicesProvider
	{
		public GameServicesProvider()
		{
			var container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage());

			var locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}

		public string GetData(int value) => throw new NotImplementedException();
	}
}
