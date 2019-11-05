using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Catel;
using Catel.Data;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
    /// <summary>
    /// Класс, инкапсулирующий в себе модель-представление "Главное окно".
    /// </summary>
    /// <seealso cref="Catel.MVVM.ViewModelBase" />
    internal class RoosterBrowserViewModel : ViewModelBase
	{
		#region Fields
		/// <summary>
		/// Сервис визуализации окон приложения.
		/// </summary>
		private readonly IUIVisualizerService _uiVisualizerService;
		/// <summary>
		/// Сервис работы с ошибками.
		/// </summary>
		private readonly IExceptionService _exceptionService;
		private readonly EditServiceClient _editServiceClient = new EditServiceClient();
		private readonly AuthenticateServiceClient _authenticateServiceClient;
		private string token;
		private Logger _roosterBrowserViewModel = LogManager.GetLogger("RoosterBrowserViewModelLogger");

		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Текущий выбранный" петух.
		/// </summary>
		public static readonly PropertyData SelectedRoosterProperty = RegisterProperty(nameof(SelectedRooster), typeof(RoosterModel));
		/// <summary>
		/// Зарегистрированное свойство петухи.
		/// </summary>
		public static readonly PropertyData RoostersProperty = RegisterProperty(nameof(Roosters), typeof(IEnumerable<RoosterModel>));
		public static readonly PropertyData SelectedIndexProperty = RegisterProperty(nameof(SelectedIndex), typeof(int));
		public static readonly PropertyData StatisticsProperty = RegisterProperty(nameof(Statistics), typeof(StatisticsModel[]));
		public static readonly PropertyData LoggenInProperty = RegisterProperty(nameof(ShowWindow), typeof(bool));
		public static readonly PropertyData UserStatiscticsProperty = RegisterProperty(nameof(UserStatistics), typeof(UserStatistic[]));
        #endregion
        #endregion

        #region Properties		        
        /// <summary>
        /// Возвращает или устанавливает значение [show window].
        /// </summary>
        /// <value>
        ///   <c>true</c> если [show window]; иначе, <c>false</c>.
        /// </value>
        public bool ShowWindow
		{
			get => GetValue<bool>(LoggenInProperty);
			set => SetValue(LoggenInProperty, value);
		}
        /// <summary>
        /// Возвращает или задает выбранный индекс.
        /// </summary>
        /// <value>
        /// Выбранный индекс.
        /// </value>
        public int SelectedIndex
		{
			get => GetValue<int>(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
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
		/// Свойство, предоставляющее команду начала сражения петухов.
		/// </summary>
		/// <value>
		/// Команда начала схватки петухов.
		/// </value>
		public ICommand FightCommand
		{
			get;
		}
		/// <summary>
		/// Свойство, предоставляющее команду редактирования выбранного петуха.
		/// </summary>
		/// <value>
		/// Команда редактирования петуха.
		/// </value>
		public ICommand EditRoosterCommand
		{
			get;
		}
		/// <summary>
		/// Свойство, предоставляющее команду удаления петуха.
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
		public RoosterBrowserViewModel(IUIVisualizerService uiVisualizerService, IExceptionService exceptionService)
		{
			_authenticateServiceClient = new AuthenticateServiceClient(new InstanceContext(new Callbacks.AuthenticationServiceCallback(this)));
			var container = this.GetServiceLocator();
			container.RegisterType<AuthenticateServiceClient>();
			container.RegisterInstance(_authenticateServiceClient);

			_uiVisualizerService = uiVisualizerService;
			_exceptionService = exceptionService;
			Roosters = new ObservableCollection<RoosterModel>();
			EditRoosterCommand = new TaskCommand(EditRoosterAsync, () => SelectedRooster != null);
			DeleteRoosterCommand = new TaskCommand(DeleteRoosterAsync, () => SelectedRooster != null);
			AddRoosterCommand = new TaskCommand(AddRoosterAsync);
			FightCommand = new TaskCommand(StartRoostersFightAsync, () => SelectedRooster != null && ShowWindow == true);
			
		}
        #endregion

        #region Protected Methods
        #region Overrided
        /// <summary>
        /// Инициализирует модель-представление.
        /// </summary>
        protected override async Task InitializeAsync()
		{
			_roosterBrowserViewModel.Info("Открытие окна авторизации пользователя.");

			await _uiVisualizerService.ShowDialogAsync<AuthViewModel>();

			token = (string) Application.Current.Resources["UserToken"];

			if (token == null)
			{
				_roosterBrowserViewModel.Error("Авторизация не пройдена!");
				Application.Current.Shutdown();
			}


			Argument.IsNotNull(nameof(_exceptionService), _exceptionService);

			UpdateRoostersAsync();


			await base.InitializeAsync();

			ShowWindow = true;

			_roosterBrowserViewModel.Info("Открыто главное окно приложения.");
		}

		/// <summary>
		/// Вызывается в момент закрытия модели-представления.
		/// <para />
		/// Этот метод также вызывает <see cref="E:Catel.MVVM.ViewModelBase.ClosingAsync" /> событие.
		/// </summary>
		protected override async Task OnClosingAsync()
		{
			await _authenticateServiceClient.LogOutAsync(token);
			_roosterBrowserViewModel.Info("Выход пользователя из сети.");
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
			_roosterBrowserViewModel.Info("Список петухов был обновлен.");
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
				_roosterBrowserViewModel.Info("Обновлено имя текущего выделенного петуха");
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
		}

		/// <summary>
		/// Открывает окно редактирования выбранного петуха в асинхронном режиме.
		/// </summary>
		/// <returns>Окно редактирования.</returns>
		private async Task EditRoosterAsync()
		{
			_roosterBrowserViewModel.Info($"Открыто окно редактирования петуха с имененм {SelectedRooster.Name}.");
			if (await _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(SelectedRooster) == true)
			{
				await _editServiceClient.EditAsync(token, SelectedRooster.Token, SelectedRooster.ToRoosterDto());
				UpdateRoosters(await _editServiceClient.GetUserRoostersAsync(token));
			}
		}

		/// <summary>
		/// Удаляет выбранного петуха.
		/// </summary>
		private async Task DeleteRoosterAsync()
		{
			_roosterBrowserViewModel.Info($"Запуск удаления петуха {SelectedRooster.Name}.");
			if (await _editServiceClient.RemoveAsync(token, SelectedRooster.Token) == false)
			{
				_roosterBrowserViewModel.Warn($"Удаление петуха {SelectedRooster.Token} пользователя {token} неуспешно");
				MessageBox.Show("Ошибка при удалении. Проверьте лог Warn");
			}
			_roosterBrowserViewModel.Info($"Петух был удален.");
			UpdateRoostersAsync();
		}

		/// <summary>
		/// Добавляет нового петуха в асинхронном режиме.
		/// </summary>
		private async Task AddRoosterAsync()
		{
			var rooster = new RoosterModel();
			_roosterBrowserViewModel.Info("Старт добавления нового петуха.");
			if (await _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(rooster) == true)
			{
				bool addRes = false;
				try
				{
					addRes = await _editServiceClient.AddAsync(token, rooster.ToRoosterDto());
				}

				catch (Exception e)
				{
					_roosterBrowserViewModel.Error(e);
				}
				
				if (addRes)
				{
					UpdateRoostersAsync();
					_roosterBrowserViewModel.Info("Новый петух добавлен в список петухов пользователя.");
				}
				else
				{
					MessageBox.Show("Ошибка при добавлении петуха");
					_roosterBrowserViewModel.Info("Ошибка при добавлении петуха");
				}

				
			}
		}

		/// <summary>
		/// Начинает сражение петухов в асинхронном режиме.
		/// </summary>
		private async Task StartRoostersFightAsync()
		{
			ShowWindow = false;
			_roosterBrowserViewModel.Info("Открытие окна битвы петухов.");
			await _uiVisualizerService.ShowDialogAsync<FightViewModel>(SelectedRooster);
			ShowWindow = true;
			UpdateRoostersAsync();
			_roosterBrowserViewModel.Info("Битва была проведена.");
		}
		#endregion
	}
}
