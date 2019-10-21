using System;

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
