using System;
using System.Collections.Generic;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.DataStorages
{
	[Serializable]
	public class ServicesStorage
	{
		public ServicesStorage()
		{
			UserData = new Dictionary<string, string>();
			RoosterData = new Dictionary<string, List<RoosterDto>>();
		}

		public Dictionary<string,string> UserData
		{
			get;
			set;
		}

		public Dictionary<string, List<RoosterDto>> RoosterData
		{
			get;
			set;
		}

		public Dictionary<string, string> Sessions
		{
			get;
			set;
		}
	}
}
