using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Events
{
	class MatchFindedEventArgs : EventArgs
	{
		public string MatchToken
		{
			get;
			set;
		}

		public MatchFindedEventArgs(string token)
		{
			MatchToken = token;
		}
	}
}
