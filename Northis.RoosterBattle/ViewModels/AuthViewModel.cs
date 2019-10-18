using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Catel.Data;
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
			AuthCommand = new TaskCommand((() => AuthenticateAsync(_authenticateServiceClient.LogInAsync)));
			RegCommand = new TaskCommand((() => AuthenticateAsync(_authenticateServiceClient.RegisterAsync)));
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
			get => GetValue<string>(PasswordProperty);
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

		private async Task AuthenticateAsync(Func<string, string, Task<string>> authMethod)
		{
			string[] excludes = Enum.GetNames(typeof(AuthenticateStatus));
			if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
			{
				MessageBox.Show("Поля логина и пароля не могут быть пустыми");
				return;
			}
			string token = await _authenticateServiceClient.RegisterAsync(Login, Password);

			AuthenticateStatus result;

			if (!Enum.TryParse(token, out result))
			{
				Application.Current.Resources.Add("UserToken", token);
				await this.SaveAndCloseViewModelAsync();
			}
			else
			{
				MessageBox.Show(token);
			}
		}
	}
}
