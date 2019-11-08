using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
		Ok
	}
}
