using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Определяет возможные ответы сервера на поиск матча.
	/// </summary>
	enum BattleStatus
	{
		[Display(ResourceType = typeof(Resources), Name = "StrErrorNotLoginedUserFind")]
		UserWasNotFound,
		[Display(ResourceType = typeof(Resources), Name = "StrErrorNotLoginedRoosterFind")]
		RoosterWasNotFound,
		[Display(ResourceType = typeof(Resources), Name = "StrErrorFightAgainstYourself")]
		SameLogins
	}
}
