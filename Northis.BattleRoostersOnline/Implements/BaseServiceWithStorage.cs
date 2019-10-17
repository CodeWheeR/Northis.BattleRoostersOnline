using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using Northis.BattleRoostersOnline.DataStorages;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	public abstract class BaseServiceWithStorage
	{
		protected ServicesStorage Storage
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
				{
					return ServiceLocator.Current.GetInstance<ServicesStorage>();
				}
				throw new NullReferenceException("Storage is null");
			}

		}

		protected BaseServiceWithStorage()
		{
			if (!ServiceLocator.IsLocationProviderSet)
			{
				var container = new UnityContainer();
				container.RegisterInstance(new ServicesStorage());

				var locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
		}
	}
}
