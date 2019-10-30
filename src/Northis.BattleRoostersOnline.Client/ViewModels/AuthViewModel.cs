using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Catel.Data;
using Catel.MVVM;
using Catel.Services;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
	internal class AuthViewModel : ViewModelBase
	{
		#region Fields
		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Модель аутентификации".
		/// </summary>
		public static readonly PropertyData AuthModelProperty = RegisterProperty(nameof(AuthModel), typeof(AuthModel));
		/// <summary>
		/// Зарегистрированное свойство "Логин"
		/// </summary>
		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));
		#endregion

		private readonly AuthenticateServiceClient _authenticateServiceClient;

		private IUIVisualizerService _uiVisualizerService;

		private readonly Dictionary<AuthenticateStatus, string> _authMessages = new Dictionary<AuthenticateStatus, string>
		{
			{
				AuthenticateStatus.AlreadyLoggedIn, "Данный пользователь уже находится в системе"
			},
			{
				AuthenticateStatus.AlreadyRegistered, "Данный пользователь уже зарегистрирован"
			},
			{
				AuthenticateStatus.WrongDataFormat, "Логин и пароль должны быть не короче 5 символов"
			},
			{
				AuthenticateStatus.WrongLoginOrPassword, "Неправильный логин или пароль"
			}
		};
		#endregion

		#region .ctor		
		/// <summary>
		/// Инициализует новый объект класса <see cref="AuthViewModel" />.
		/// </summary>
		/// <param name="uiVisualizerService">The UI visualizer service.</param>
		/// <param name="authService">Объект подключения к серверу по каналу авторизации.</param>
		public AuthViewModel(IUIVisualizerService uiVisualizerService, AuthenticateServiceClient authService)
		{
			_authenticateServiceClient = authService;
			_uiVisualizerService = uiVisualizerService;
			AuthCommand = new TaskCommand<PasswordBox>(passwordBox => AuthenticateAsync(_authenticateServiceClient.LogInAsync, passwordBox));
			RegCommand = new TaskCommand<PasswordBox>(passwordBox => AuthenticateAsync(_authenticateServiceClient.RegisterAsync, passwordBox));
		}
		#endregion

		#region Properties		
		/// <summary>
		/// Возвращает или устанавливает модель авторизации.
		/// </summary>
		[Model]
		public AuthModel AuthModel
		{
			get => GetValue<AuthModel>(AuthModelProperty);
			set => SetValue(AuthModelProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает логин пользователя.
		/// </summary>
		[ViewModelToModel(nameof(AuthModel))]
		public string Login
		{
			get => GetValue<string>(LoginProperty);
			set => SetValue(LoginProperty, value);
		}

		/// <summary>
		/// Возвращает команду для авторизации.
		/// </summary>
		public ICommand AuthCommand
		{
			get;
		}

		/// <summary>
		/// Возвращает команду для регистрации.
		/// </summary>
		public ICommand RegCommand
		{
			get;
		}
		#endregion

		#region Private Methods		
		/// <summary>
		/// Выполняет переданный способ аутентификации на сервере.
		/// </summary>
		/// <param name="authMethod">The authentication method.</param>
		/// <param name="passwordBox">The password box.</param>
		private async Task AuthenticateAsync(Func<string, string, Task<string>> authMethod, PasswordBox passwordBox)
		{
			var password = passwordBox.Password;
			if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Поля логина и пароля не могут быть пустыми");
				return;
			}

			string token = "";
			try
			{
				token = await authMethod(Login, password);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}

			//var token = await authMethod(Login, password);

			AuthenticateStatus result;

			if (!Enum.TryParse(token, out result))
			{
				Application.Current.Resources.Add("UserToken", token);
				await this.SaveAndCloseViewModelAsync();
			}
			else
			{
				if (_authMessages.ContainsKey(result))
				{
					MessageBox.Show(_authMessages[result]);
				}
				else
				{
					MessageBox.Show(token);
				}
			}
		}
		#endregion
	}
}
