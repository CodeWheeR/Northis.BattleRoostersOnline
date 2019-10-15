using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.DataStorages
{
	[Serializable]
	public class ServicesStorage
	{
		public ServicesStorage()
		{
			UserData = new Dictionary<string, string>();
			LoggedUsers = new Dictionary<string, RoosterDto[]>();
		}

		public IEnumerable<KeyValuePair<string, string>> UserData
		{
			get;
			set;
		}

		public IEnumerable<KeyValuePair<string, RoosterDto[]>> LoggedUsers
		{
			get;
			set;
		}

		public IEnumerable<KeyValuePair<string, string>> Sessions
		{
			get;
			set;
		}
	}
}
