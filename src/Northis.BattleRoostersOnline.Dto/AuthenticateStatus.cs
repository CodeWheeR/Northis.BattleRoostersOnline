using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет возможные ответа сервиса аутентификации.
	/// </summary>
	[DataContract]
	public enum AuthenticateStatus
	{
		[EnumMember]
		Ok,
		[EnumMember]
		WrongLoginOrPassword,
		[EnumMember]
		AlreadyRegistered,
		[EnumMember]
		AlreadyLoggedIn,
		[EnumMember]
		WrongDataFormat,
		[EnumMember]
		AuthorizationDenied
	}
}
