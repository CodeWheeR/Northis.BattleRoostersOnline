using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Extensions;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.Properties;
using AuthenticateStatus = Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
	/// <summary>
	/// Представляет модель-представление "Аутентификация".
	/// </summary>
	/// <seealso cref="ViewModelBase" />
	internal class AuthViewModel : ViewModelBase
	{
		#region Fields
		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Модель аутентификации".
		/// </summary>
		public static readonly PropertyData AuthModelProperty = RegisterProperty(nameof(AuthModel), typeof(AuthModel));
		/// <summary>
		/// Зарегистрированное свойство "Логин".
		/// </summary>
		public static readonly PropertyData LoginProperty = RegisterProperty(nameof(Login), typeof(string));
		#endregion

		private readonly AuthenticateServiceClient _authenticateServiceClient;

		private IUIVisualizerService _uiVisualizerService;

		private readonly Logger _logger = LogManager.GetLogger("AuthViewModelLogger");

		private readonly IMessageService _messageService;

		private readonly IMapper _mapper;
		#endregion

		#region .ctor		
		/// <summary>
		/// Инициализует новый объект <see cref="AuthViewModel" /> класса.
		/// </summary>
		/// <param name="uiVisualizerService">Сервис визуализации.</param>
		/// <param name="authService">Объект подключения к серверу по каналу авторизации.</param>
		public AuthViewModel(IUIVisualizerService uiVisualizerService, AuthenticateServiceClient authService)
		{
			_authenticateServiceClient = authService;
			_uiVisualizerService = uiVisualizerService;
			AuthCommand = new TaskCommand<PasswordBox>(passwordBox => AuthenticateAsync(_authenticateServiceClient.LogInAsync, passwordBox));
			RegCommand = new TaskCommand<PasswordBox>(passwordBox => AuthenticateAsync(_authenticateServiceClient.RegisterAsync, passwordBox));

			_logger.Info(Resources.StrInfoOpeningAuthorizationWindow);

			var container = this.GetServiceLocator();
			_messageService = container.ResolveType<IMessageService>();
			_mapper = container.ResolveType<IMapper>();
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
		/// <param name="authMethod">Метод аутентификации.</param>
		/// <param name="passwordBox">Хранилище и обработчик паролей.</param>
		private async Task AuthenticateAsync(Func<string, string, Task<string>> authMethod, PasswordBox passwordBox)
		{
			var password = passwordBox.Password;
			if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(password))
			{
				_logger.Warn(Resources.StrWarnStringsCannotBeEmpty);
				await _messageService.ShowAsync("Поля логина и пароля не могут быть пустыми", "Предупреждение");
				return;
			}

			var token = "";
			try
			{
				token = await authMethod(Login, password);
			}
			catch (CommunicationException)
			{
				_messageService.ShowErrorAsync("Не удалось установить соединение с сервером. Пожалуйста, повторите попытку позже.");
				return;
			}
			catch (Exception e)
			{
				_logger.Error(e);
				await _messageService.ShowErrorAsync(e.ToString());
				return;
			}

			string[] splitedString = token.Split(' ');



			if (!Enum.TryParse(splitedString[0], out GameServer.AuthenticateStatus result))
			{
				Application.Current.Resources.Add("UserToken", splitedString[0]);
				await this.SaveAndCloseViewModelAsync();
			}
			else
			{
				string answer = _mapper.Map<GameServer.AuthenticateStatus, Models.AuthenticateStatus>(result)
									   .GetDisplayFromResource();
				if (splitedString.Length > 1)
				{
					answer += " Оставшееся время ожидания: " + splitedString[1] + " сек.";
				}
				await _messageService.ShowAsync(answer);
			}
		}
		#endregion
	}
}
