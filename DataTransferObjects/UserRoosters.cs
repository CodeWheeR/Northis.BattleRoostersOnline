using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects
{
	[DataContract]
	public struct UserRoosters
	{
		[DataMember]
		public string ID;

		[DataMember]
		public RoosterDto[] roosters;


		public UserRoosters(KeyValuePair<string, List<RoosterDto>> roosterDictionary)
		{
			ID = roosterDictionary.Key;
			roosters = roosterDictionary.Value.ToArray();
		}
	}
}
