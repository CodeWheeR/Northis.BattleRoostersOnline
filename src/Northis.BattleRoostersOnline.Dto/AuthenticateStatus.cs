using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных. Передает пользователю статус аунтефикации.
	/// </summary>
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
