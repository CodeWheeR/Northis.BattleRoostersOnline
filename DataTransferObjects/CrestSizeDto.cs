using System.Runtime.Serialization;

namespace DataTransferObjects
{
	[DataContract]
	public enum CrestSizeDto
	{
		[EnumMember]
		Small,
		[EnumMember]
		Medium,
		[EnumMember]
		Big
	}
}
