using System.Threading.Tasks;
using System.Windows.Input;
using Catel.Data;
using Catel.MVVM;
using Northis.RoosterBattle.GameServer;

namespace Northis.RoosterBattle.Models
{
	/// <summary>
	/// Модель пользовательских данных для авторизации
	/// </summary>
	/// <seealso cref="Catel.Data.ValidatableModelBase" />
	class AuthModel : ValidatableModelBase
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

		#region Public Methods
		public string Password
		{
			get => GetValue<string>(PasswordProperty);
			set => SetValue(PasswordProperty, value);
		}

		public string Login
		{
			get => GetValue<string>(LoginProperty);
			set => SetValue(LoginProperty, value);
		}
		#endregion
	}
}
