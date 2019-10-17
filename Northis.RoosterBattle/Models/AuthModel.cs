using System.Threading.Tasks;
using System.Windows.Input;
using Catel.Data;
using Catel.MVVM;
using Northis.RoosterBattle.GameServer;

namespace Northis.RoosterBattle.Models
{
	class AuthModel : ValidatableModelBase
	{
		private AuthenticateServiceClient _authenticateServiceClient = new AuthenticateServiceClient();

		private EditServiceClient _editServiceClient = new EditServiceClient();


		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));

		public static readonly PropertyData PasswordProperty = RegisterProperty(nameof(Password), typeof(string));

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
	}
}
