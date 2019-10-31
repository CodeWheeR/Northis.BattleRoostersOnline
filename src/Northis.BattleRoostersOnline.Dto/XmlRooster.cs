using System.Collections.Generic;
using System.Runtime.Serialization;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline
{
	[DataContract]
	public class XmlRooster
	{
		[DataMember]
		public int UserID
		{
			get;
			set;
		}
		[DataMember]
		public List<RoosterDto> Roosters
		{
			get;
			set;
		}

		public XmlRooster(int userID, List<RoosterDto> roosters)
		{
			UserID = userID;
			Roosters = roosters;
		}
	}
}
