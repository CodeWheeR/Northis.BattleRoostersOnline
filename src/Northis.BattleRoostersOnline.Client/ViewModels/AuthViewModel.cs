using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Windows.Input;
using System.Windows.Markup;
using AutoMapper;
using Catel.Data;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Extensions;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
    /// <summary>
    /// Представляет модель-представление "Аунтефикация".
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

		private Logger _logger = LogManager.GetLogger("AuthViewModelLogger");

		private IMessageService _messageService;

		private IMapper _mapper;
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
			_messageService =container.ResolveType<IMessageService>();
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
				await _messageService.ShowAsync("Поля логина и пароля не могут быть пустыми","Предупреждение");
				return;
			}

			string token = "";
			try
			{
				token = await authMethod(Login, password);
			}
			catch (Exception e)
			{
				_logger.Error(e);
				await _messageService.ShowErrorAsync(e.ToString());
			}


			if (!Enum.TryParse(token, out GameServer.AuthenticateStatus result))
			{
				Application.Current.Resources.Add("UserToken", token);
				await this.SaveAndCloseViewModelAsync();
			}
			else
			{
				
				await _messageService.ShowAsync(_mapper.Map<GameServer.AuthenticateStatus, Models.AuthenticateStatus>(result).GetDisplayFromResource());
			}
		}
		#endregion
	}
}
