using Catel.Data;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Представляет модель пользовательских данных для авторизации.
	/// </summary>
	/// <seealso cref="Catel.Data.ValidatableModelBase" />
	internal class AuthModel : ValidatableModelBase
	{
		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Логин" пользователя.
		/// </summary>
		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));
		/// <summary>
		/// Зарегистрированное свойство "Пароль" пользователя.
		/// </summary>
		public static readonly PropertyData PasswordProperty = RegisterProperty(nameof(Password), typeof(string));
		#endregion

		#region Public Property		
		/// <summary>
		/// Возвращает или задает пароль.
		/// </summary>
		/// <value>
		/// Пароль.
		/// </value>
		public string Password
		{
			get => GetValue<string>(PasswordProperty);
			set => SetValue(PasswordProperty, value);
		}

		/// <summary>
		/// Возвращает или задает логин.
		/// </summary>
		/// <value>
		/// Логин.
		/// </value>
		public string Login
		{
			get => GetValue<string>(LoginProperty);
			set => SetValue(LoginProperty, value);
		}
		#endregion
	}
}
