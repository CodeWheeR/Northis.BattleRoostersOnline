using System.Runtime.Serialization;

namespace DataTransferObjects
{
	[DataContract]
	public enum RoosterColorDto
	{
		[EnumMember]
		Black,
		[EnumMember]
		Brown,
		[EnumMember]
		Blue,
		[EnumMember]
		Red,
		[EnumMember]
		White
	}
}
