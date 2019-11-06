using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных, передающее пользователю статус аунтефикации.
	/// </summary>
	[DataContract]
	public enum AuthenticateStatus
	{
		[EnumMember, Display(Name = "Успешная аунтефикация")]
		OK,
		[EnumMember, Display(Name = "Неправильный логин или пароль")]
		WrongLoginOrPassword,
		[EnumMember, Display(Name = "Данный пользователь уже зарегистрирован")]
		AlreadyRegistered,
		[EnumMember, Display(Name = "Данный пользователь уже находится в системе")]
		AlreadyLoggedIn,
		[EnumMember, Display(Name = "Логин и пароль должны быть не короче 5 символов")]
		WrongDataFormat
	}
}
