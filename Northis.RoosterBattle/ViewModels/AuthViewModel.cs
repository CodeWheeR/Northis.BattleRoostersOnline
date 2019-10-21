using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

		private AuthenticateServiceClient _authenticateServiceClient = new AuthenticateServiceClient();

		private IUIVisualizerService _uiVisualizerService;



		public AuthViewModel(IUIVisualizerService uiVisualizerService)
		{
			_uiVisualizerService = uiVisualizerService;
			AuthCommand = new TaskCommand<PasswordBox>(((passwordBox) => AuthenticateAsync(_authenticateServiceClient.LogInAsync, passwordBox)));
			RegCommand = new TaskCommand<PasswordBox>(((passwordBox) => AuthenticateAsync(_authenticateServiceClient.RegisterAsync, passwordBox)));
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

		public ICommand AuthCommand
		{
			get;
		}

		public ICommand RegCommand
		{
			get;
		}

		private async Task AuthenticateAsync(Func<string, string, Task<string>> authMethod, PasswordBox passwordBox)
		{
			string password = passwordBox.Password;
			if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Поля логина и пароля не могут быть пустыми");
				return;
			}
			string token = await authMethod(Login, password);

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
