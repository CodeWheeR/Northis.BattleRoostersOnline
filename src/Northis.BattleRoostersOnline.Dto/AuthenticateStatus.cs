using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных. Передает пользователю статус аунтефикации.
	/// </summary>
	[DataContract]
	public enum AuthenticateStatus
	{
		/// <summary>
		/// Авторизация успешна.
		/// </summary>
		[EnumMember]
		Ok,
		/// <summary>
		/// Неправильный логин или пароль.
		/// </summary>
		[EnumMember]
		WrongLoginOrPassword,
		/// <summary>
		/// Данный пользователь уже зарегистрирован.
		/// </summary>
		[EnumMember]
		AlreadyRegistered,
		/// <summary>
		/// Данный пользователь уже в системе.
		/// </summary>
		[EnumMember]
		AlreadyLoggedIn,
		/// <summary>
		/// Неверный формат введенных логина или пароля.
		/// </summary>
		[EnumMember]
		WrongDataFormat
	}
}
