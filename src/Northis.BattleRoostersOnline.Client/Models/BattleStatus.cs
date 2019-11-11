using System.ComponentModel.DataAnnotations;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Определяет возможные ответы сервера на поиск матча.
	/// </summary>
	internal enum BattleStatus
	{
		[Display(ResourceType = typeof(Resources), Name = "StrErrorNotLoginedUserFind")]
		UserWasNotFound,
		[Display(ResourceType = typeof(Resources), Name = "StrErrorNotLoginedRoosterFind")]
		RoosterWasNotFound,
		[Display(ResourceType = typeof(Resources), Name = "StrErrorFightAgainstYourself")]
		SameLogins,
		[Display(ResourceType = typeof(Resources), Name = "StrAccessDenied")]
		AccessDenied
	}
}
