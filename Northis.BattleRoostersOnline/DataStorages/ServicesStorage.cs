using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
			RoostersData = new Dictionary<string, List<RoosterDto>>();
			LoggedUsers = new Dictionary<string, string>();
		}

		public Dictionary<string, string> UserData
		{
			get;
			set;
		}

		public Dictionary<string, List<RoosterDto>> RoostersData
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the logged users.
		/// Token - key, Login - Value
		/// </summary>
		/// <value>
		/// The logged users.
		/// </value>
		public Dictionary<string, string> LoggedUsers
		{
			get;
			set;
		}
	}
}
