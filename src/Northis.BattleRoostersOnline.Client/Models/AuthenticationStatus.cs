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
	/// <summary>
	/// Определяет описание ответов сервера аутентификации, используя строковые ресурсы.
	/// </summary>
	public enum AuthenticateStatus
	{
		/// <summary>
		/// Авторизация успешна.
		/// </summary>
		[Display(ResourceType = typeof(Resources), Name = "StrOK")]
		Ok,
		/// <summary>
		/// Неправильный логин или пароль.
		/// </summary>
		[Display(ResourceType = typeof(Resources), Name = "StrWrongLoginOrPassword")]
		WrongLoginOrPassword,
		/// <summary>
		/// Пользователь уже зарегистрирован.
		/// </summary>
		[Display(ResourceType = typeof(Resources), Name = "StrAlreadyRegistered")]
		AlreadyRegistered,
		/// <summary>
		/// Пользователь уже в сети.
		/// </summary>
		[Display(ResourceType = typeof(Resources), Name = "StrAlreadyLoggedIn")]
		AlreadyLoggedIn,
		/// <summary>
		/// Не соответствующий формат введенных данных.
		/// </summary>
		[Display(ResourceType = typeof(Resources), Name = "StrWrongDataFormat")]
		WrongDataFormat
	}
}
