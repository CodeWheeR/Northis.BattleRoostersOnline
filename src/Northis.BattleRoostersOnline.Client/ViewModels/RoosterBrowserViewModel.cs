using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using Catel;
using Catel.Data;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Callbacks;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.Properties;
using AuthenticateStatus = Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
	/// <summary>
	/// Обеспечивает взаимодействие модели RoosterModel и представления RoosterBrowserWindow.
	/// </summary>
	/// <seealso cref="Catel.MVVM.ViewModelBase" />
	internal class RoosterBrowserViewModel : ViewModelBase
	{
		#region Fields
		private readonly IMapper _mapper;
		/// <summary>
		/// Сервис визуализации окон приложения.
		/// </summary>
		private readonly IUIVisualizerService _uiVisualizerService;
		/// <summary>
		/// Сервис работы с ошибками.
		/// </summary>
		private readonly IExceptionService _exceptionService;
		private readonly IMessageService _messageService;
		private readonly EditServiceClient _editServiceClient = new EditServiceClient();
		private readonly AuthenticateServiceClient _authenticateServiceClient;
		private string token;
		private readonly Logger _logger = LogManager.GetLogger("RoosterBrowserViewModelLogger");

		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Текущий выбранный" петух.
		/// </summary>
		public static readonly PropertyData SelectedRoosterProperty = RegisterProperty(nameof(SelectedRooster), typeof(RoosterModel));
		/// <summary>
		/// Зарегистрированное свойство списка петухов.
		/// </summary>
		public static readonly PropertyData RoostersProperty = RegisterProperty(nameof(Roosters), typeof(IEnumerable<RoosterModel>));
		/// <summary>
		/// Зарегистрированное свойство статистики.
		/// </summary>
		public static readonly PropertyData StatisticsProperty = RegisterProperty(nameof(Statistics), typeof(StatisticsModel[]));
		/// <summary>
		/// Зарегистрированное свойство вхождения в сеть.
		/// </summary>
		public static readonly PropertyData LoggenInProperty = RegisterProperty(nameof(ShowWindow), typeof(bool));
		/// <summary>
		/// Зарегистрированное свойство статистики пользователя.
		/// </summary>
		public static readonly PropertyData UserStatiscticsProperty = RegisterProperty(nameof(UserStatistics), typeof(UserStatistic[]));
		/// <summary>
		/// Зарегистрированное свойство разрешения добавления нового петуха.
		/// </summary>
		public static readonly PropertyData IsAddButtonEnableProperty = RegisterProperty(nameof(IsAddButtonEnable), typeof(bool));
		#endregion
		#endregion

		#region Properties		  		
		/// <summary>
		/// Возвращает или устанавливает флаг разрешения на добавление нового петуха.
		/// </summary>
		/// <value>
		/// <c>true</c> если петухов меньше 3, иначе <c>false</c>.
		/// </value>
		public bool IsAddButtonEnable
		{
			get => GetValue<bool>(IsAddButtonEnableProperty);
			set => SetValue(IsAddButtonEnableProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает значение пройдена ли авторизация.
		/// </summary>
		/// <value>
		/// <c>true</c> если авторизация пройдена, иначе <c>false</c>.
		/// </value>
		public bool ShowWindow
		{
			get => GetValue<bool>(LoggenInProperty);
			set => SetValue(LoggenInProperty, value);
		}

		/// <summary>
		/// Возвращает или задает статистику.
		/// </summary>
		/// <value>
		/// Статистика.
		/// </value>
		public StatisticsModel[] Statistics
		{
			get => GetValue<StatisticsModel[]>(StatisticsProperty);
			set => SetValue(StatisticsProperty, value);
		}

		/// <summary>
		/// Возвращает или задает статистику пользователя.
		/// </summary>
		/// <value>
		/// Статистика пользователя.
		/// </value>
		public UserStatistic[] UserStatistics
		{
			get => GetValue<UserStatistic[]>(UserStatiscticsProperty);
			set => SetValue(UserStatiscticsProperty, value);
		}

		/// <summary>
		/// Предоставляет команду начала сражения петухов.
		/// </summary>
		/// <value>
		/// Команда начала схватки петухов.
		/// </value>
		public ICommand FightCommand
		{
			get;
		}

		/// <summary>
		/// Предоставляет команду удаления петуха.
		/// </summary>
		/// <value>
		/// Команда удаления петуха.
		/// </value>
		public ICommand DeleteRoosterCommand
		{
			get;
		}

		/// <summary>
		/// Свойство, предоставляющее команду добавления нового петуха.
		/// </summary>
		/// <value>
		/// Команда добавления петуха.
		/// </value>
		public ICommand AddRoosterCommand
		{
			get;
		}

		/// <summary>
		/// Свойство, предоставляющее или устанавливающее коллекцию петухов.
		/// </summary>
		/// <value>
		/// Коллекция петухов.
		/// </value>
		public IEnumerable<RoosterModel> Roosters
		{
			get => GetValue<ObservableCollection<RoosterModel>>(RoostersProperty);
			set => SetValue(RoostersProperty, value);
		}

		/// <summary>
		/// Свойство, предоставляющее или устанавливающее текущего выбранного петуха.
		/// </summary>
		/// <value>
		/// Текущий выбранный петух.
		/// </value>
		[Model]
		public RoosterModel SelectedRooster
		{
			get => GetValue<RoosterModel>(SelectedRoosterProperty);
			set => SetValue(SelectedRoosterProperty, value);
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="RoosterBrowserViewModel" /> класса.
		/// </summary>
		/// <param name="roosterKeepService">Сервис сохранения и загрузки петухов.</param>
		/// <param name="uiVisualizerService">Сервис визуализации окон приложения.</param>
		/// <param name="exceptionService">Сервис обработки ошибок.</param>
		/// <param name="mapper">Конфигурация IMapper.</param>
		public RoosterBrowserViewModel(IUIVisualizerService uiVisualizerService, IExceptionService exceptionService, IMapper mapper)
		{
			_mapper = mapper;
			_authenticateServiceClient = new AuthenticateServiceClient(new InstanceContext(new AuthenticationServiceCallback(this)));
			var container = this.GetServiceLocator();
			container.RegisterType<AuthenticateServiceClient>();
			container.RegisterInstance(_authenticateServiceClient);

			_uiVisualizerService = uiVisualizerService;
			_exceptionService = exceptionService;
			_messageService = container.ResolveType<IMessageService>();
			Roosters = new ObservableCollection<RoosterModel>();
			DeleteRoosterCommand = new TaskCommand(DeleteRoosterAsync, () => SelectedRooster != null);
			AddRoosterCommand = new TaskCommand(AddRoosterAsync, () => IsAddButtonEnable);
			FightCommand = new TaskCommand(StartRoostersFightAsync, () => SelectedRooster != null && ShowWindow);
		}
		#endregion

		#region Protected Methods
		#region Overrided
		/// <summary>
		/// Инициализирует модель-представление.
		/// </summary>
		protected override async Task InitializeAsync()
		{
			_logger.Info(Resources.StrInfoOpeningAuthorizationWindow);

			await _uiVisualizerService.ShowDialogAsync<AuthViewModel>();

			token = (string) Application.Current.Resources["UserToken"];

			if (token == null)
			{
				_logger.Error(Resources.StrErrorAuthorizationNotSuccess);
				Application.Current.Shutdown();
			}

			Task.Run(async () =>
			{
				try
				{
					while (_authenticateServiceClient.GetLoginStatus() == AuthenticateStatus.Ok)
					{
						await Task.Delay(3000);
					}
				}
				catch (Exception e)
				{
					await _messageService.ShowErrorAsync("Потеряно соединение с сервером. Пожалуйста, перезайдите в игру.");
					Application.Current.Shutdown();
				}
			});

			Argument.IsNotNull(nameof(_exceptionService), _exceptionService);
			UpdateRoostersAsync();

			await base.InitializeAsync();
			ShowWindow = true;

			_logger.Info(Resources.StrInfoMainWindowAppOpened);
		}

		/// <summary>
		/// Вызывается в момент закрытия модели-представления.
		/// <para />
		/// Этот метод также вызывает <see cref="E:Catel.MVVM.ViewModelBase.ClosingAsync" /> событие.
		/// </summary>
		protected override async Task OnClosingAsync()
		{
			await _authenticateServiceClient.LogOutAsync(token);
			_logger.Info(Resources.StrInfoLeaveUser);
			await base.OnClosingAsync();
		}
		#endregion
		#endregion

		#region Private Methods		
		/// <summary>
		/// Асинхронно запрашивает список петухов с сервера.
		/// </summary>
		private async void UpdateRoostersAsync()
		{
			UpdateRoosters(await _editServiceClient.GetUserRoostersAsync(token));
			_logger.Info(Resources.StrInfoRoostersWasUpdated);
		}

		/// <summary>
		/// Обновляет список петухов в соответствии и заданной коллекцией.
		/// </summary>
		/// <param name="roosters">Полученный список петухов.</param>
		private void UpdateRoosters(IEnumerable<RoosterDto> roosters)
		{
			var selectedRoosterToken = "";

			if (SelectedRooster != null)
			{
				_logger.Info(Resources.StrInfoSelectedRoostersNameUpdate);
				selectedRoosterToken = SelectedRooster.Token;
			}

			((ObservableCollection<RoosterModel>) Roosters).Clear();

			foreach (var rooster in roosters)
			{
				var newRooster = new RoosterModel(rooster);
				if (selectedRoosterToken != "" && newRooster.Token == selectedRoosterToken)
				{
					SelectedRooster = newRooster;
				}

				((ObservableCollection<RoosterModel>) Roosters).Add(newRooster);
			}

			if (Roosters.Count() == 3)
			{
				IsAddButtonEnable = false;
			}
			else
			{
				IsAddButtonEnable = true;
			}
		}

		/// <summary>
		/// Удаляет выбранного петуха.
		/// </summary>
		private async Task DeleteRoosterAsync()
		{
			_logger.Info($"Запуск удаления петуха {SelectedRooster.Name}.");
			if (await _editServiceClient.RemoveAsync(token, SelectedRooster.Token) == false)
			{
				_logger.Warn($"Удаление петуха {SelectedRooster.Token} пользователя {token} неуспешно");
				await _messageService.ShowAsync("Ошибка при удалении. Проверьте лог Warn");
			}

			_logger.Info(Resources.StrInfoRoosterDeleted);
			UpdateRoostersAsync();
		}

		/// <summary>
		/// Добавляет нового петуха в асинхронном режиме.
		/// </summary>
		private async Task AddRoosterAsync()
		{
			var rooster = new RoosterModel();
			_logger.Info(Resources.StrInfoStartRoosterAdding);
			if (await _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(rooster) == true)
			{
				var addRes = false;
				try
				{
					addRes = await _editServiceClient.AddAsync(token, _mapper.Map<RoosterModel, RoosterCreateDto>(rooster));
				}

				catch (Exception e)
				{
					_logger.Error(e);
				}

				if (addRes)
				{
					UpdateRoostersAsync();
					_logger.Info(Resources.StrInfoNewRoosterAdded);
				}
				else
				{
					_logger.Info(Resources.StrInfoRoosterNotAdded);
					if (Roosters.Count() == 3)
					{
						await _messageService.ShowAsync("Добавление безуспешно. Превышено максимальное количество доступных петухов. ");
					}
					else
					{
						await _messageService.ShowAsync("Ошибка при добавлении петуха");
					}
				}
			}
		}

		/// <summary>
		/// Начинает сражение петухов в асинхронном режиме.
		/// </summary>
		private async Task StartRoostersFightAsync()
		{
			ShowWindow = false;
			_logger.Info(Resources.StrInfoFightWindowOpened);
			await _uiVisualizerService.ShowDialogAsync<FightViewModel>(SelectedRooster);
			ShowWindow = true;
			UpdateRoostersAsync();
			_logger.Info(Resources.StrInfoFightCompleted);
		}
		#endregion
	}
}
