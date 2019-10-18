using System.Runtime.Serialization;

namespace DataTransferObjects
{
	[DataContract]
	public enum AuthenticateStatus
	{
		[EnumMember]
		OK,
		[EnumMember]
		WrongLoginOrPassword,
		[EnumMember]
		AlreadyRegistered,
		[EnumMember]
		AlreadyLoggedIn,
		[EnumMember]
		WrongDataFormat
	}
}
