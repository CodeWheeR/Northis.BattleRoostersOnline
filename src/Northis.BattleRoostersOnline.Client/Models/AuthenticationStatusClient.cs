using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.Models
{
	public enum AuthenticateStatusClient
	{
		[Display(ResourceType = typeof(Resources), Name = "StrOK")]
		OK,
		[Display(ResourceType = typeof(Resources), Name = "StrWrongLoginOrPassword")]
		WrongLoginOrPassword,
		[Display(ResourceType = typeof(Resources), Name = "StrAlreadyRegistered")]
		AlreadyRegistered,
		[Display(ResourceType = typeof(Resources), Name = "StrAlreadyLoggedIn")]
		AlreadyLoggedIn,
		[Display(ResourceType = typeof(Resources), Name = "StrWrongDataFormat")]
		WrongDataFormat
	}
}
