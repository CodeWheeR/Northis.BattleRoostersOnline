using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Предоставляет возможные ответы сервиса боя.
	/// </summary>
	[DataContract]
	public enum BattleStatus
	{
		[EnumMember]
		UserWasNotFound,
		[EnumMember]
		RoosterWasNotFound,
		[EnumMember]
		SameLogins,
		[EnumMember]
		Ok,
		[EnumMember]
		AccessDenied
	}
}
