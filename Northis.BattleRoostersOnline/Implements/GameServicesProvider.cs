using CommonServiceLocator;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	
	public class GameServicesProvider : IGameServicesProvider
	{
		
		public string GetData(int value)
		{
			return string.Empty;
		}

	}
}
