using System.Threading.Tasks;
using System.Windows.Input;
using Catel.Data;
using Catel.MVVM;

namespace Northis.RoosterBattle.Models
{
    class AuthModel : ValidatableModelBase
	{
		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));

		public static readonly PropertyData PasswordProperty = RegisterProperty(nameof(Password), typeof(string));

		public AuthModel()
		{
			//AuthCommand = new TaskCommand((() => Authorization()), () => true );
		}

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


		public ICommand AuthCommand
		{
			get;
		}


		//private Task Authorization()
		//{


		//}



	}
}
