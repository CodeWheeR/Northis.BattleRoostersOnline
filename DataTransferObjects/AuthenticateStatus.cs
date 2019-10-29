using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.DataTransferObjects
{
	/// <summary>
	/// Перечисление-контракт данных, передающее пользователю статус аунтефикации.
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
