using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.DataStorages
{
	public class ServicesStorage
	{
		public ServicesStorage()
		{
			UserData = new Dictionary<string, string>();
		}

		public IEnumerable<KeyValuePair<string, string>> UserData
		{
			get;
			set;
		}
	}
}
