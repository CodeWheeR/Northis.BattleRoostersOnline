using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Northis.RoosterBattle.Models;
using Northis.RoosterBattle.GameServer;

namespace Northis.RoosterBattle.ViewModels
{
    class AuthViewModel : ViewModelBase
	{
		public static readonly PropertyData AuthModelProperty = RegisterProperty(nameof(AuthModel), typeof(AuthModel));

		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));

		public static readonly PropertyData PasswordProperty = RegisterProperty(nameof(Password), typeof(string));

		private AuthenticateServiceClient _authenticateServiceClient = new AuthenticateServiceClient();

		private IUIVisualizerService _uiVisualizerService;




		public AuthViewModel(IUIVisualizerService uiVisualizerService)
		{
			if (MessageBox.Show("Do you speak Russian?", "Select language", MessageBoxButton.YesNo) == MessageBoxResult.No)
			{
				CultureInfo.CurrentUICulture = new CultureInfo("en-US", false);
			}
			_uiVisualizerService = uiVisualizerService;
			AuthCommand = new TaskCommand((() => AuthorizationAsync()), () => true);
			RegCommand = new TaskCommand((() => RegisterAsync()), (() => true));
		}


		[Model]
		public AuthModel AuthModel
		{
			get => GetValue<AuthModel>(AuthModelProperty);
			set => SetValue(AuthModelProperty, value);
		}

		[ViewModelToModel(nameof(AuthModel))]
		public string Login
		{
			get => GetValue<string>(LoginProperty);
			set => SetValue(LoginProperty, value);
		}

		[ViewModelToModel(nameof(AuthModel))]
		public string Password
		{
			get => GetValue<string>(LoginProperty);
			set => SetValue(PasswordProperty, value);
		}

		public ICommand AuthCommand
		{
			get;
		}

		public ICommand RegCommand
		{
			get;
		}

		private async Task RegisterAsync()
		{
			string result = await _authenticateServiceClient.RegisterAsync(Login, Password);
			MessageBox.Show(result);
		}

		private async Task AuthorizationAsync()
		{
			string result = await _authenticateServiceClient.LogInAsync(Login, Password);
			MessageBox.Show(result);
		}

		

    }
}
