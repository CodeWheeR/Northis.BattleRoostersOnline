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
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.ViewModels
{
	/// <summary>
	/// Обеспечивает взаимодействие модели RoosterModel и представления RoosterBrowserWindow.
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
		#endregion
		#endregion

		#region Properties		

		public bool ShowWindow
		{
			get => GetValue<bool>(LoggenInProperty);
			set => SetValue(LoggenInProperty, value);
		}

		public int SelectedIndex
		{
			get => GetValue<int>(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
		}

		public StatisticsModel[] Statistics
		{
			get => GetValue<StatisticsModel[]>(StatisticsProperty);
			set => SetValue(StatisticsProperty, value);
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
		/// <summary>
		/// Инициализирует модель-представление.
		/// </summary>
		protected override async Task InitializeAsync()
		{
			await _uiVisualizerService.ShowDialogAsync<AuthViewModel>();
			token = (string) Application.Current.Resources["UserToken"];

			if (token == null)
			{
				Application.Current.Shutdown();
			}


			Argument.IsNotNull(nameof(_exceptionService), _exceptionService);

			UpdateRoostersAsync();


			await base.InitializeAsync();
			ShowWindow = true;
		}

		/// <summary>
		/// Called when the view model is about to be closed.
		/// <para />
		/// This method also raises the <see cref="E:Catel.MVVM.ViewModelBase.ClosingAsync" /> event.
		/// </summary>
		protected override async Task OnClosingAsync()
		{
			await _authenticateServiceClient.LogOutAsync(token);
			await base.OnClosingAsync();
		}
		#endregion

		#region Private Methods		
		/// <summary>
		/// Асинхронно запрашивает список петухов с сервера.
		/// </summary>
		private async void UpdateRoostersAsync()
		{
			UpdateRoosters(await _editServiceClient.GetUserRoostersAsync(token));
		}

		/// <summary>
		/// Обновляет список петухов в соответствии и заданной коллекцией.
		/// </summary>
		/// <param name="roosters">Полученный список петухов.</param>
		private void UpdateRoosters(IEnumerable<RoosterDto> roosters)
		{
			var selectedRoosterName = "";
			if (SelectedRooster != null)
			{
				selectedRoosterName = SelectedRooster.Name;
			}

			((ObservableCollection<RoosterModel>) Roosters).Clear();

			foreach (var rooster in roosters)
			{
				var newRooster = new RoosterModel(rooster);
				if (selectedRoosterName != "" && newRooster.Name == selectedRoosterName)
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
			var sourceRooster = SelectedRooster.ToRoosterDto();
			await _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(SelectedRooster);
			await _editServiceClient.EditAsync(token, SelectedRooster.Token, SelectedRooster.ToRoosterDto());
			UpdateRoosters(await _editServiceClient.GetUserRoostersAsync(token));
		}

		/// <summary>
		/// Удаляет выбранного петуха.
		/// </summary>
		/// <returns></returns>
		private async Task DeleteRoosterAsync()
		{
			await _editServiceClient.RemoveAsync(token, SelectedRooster.Token);
			UpdateRoostersAsync();
		}

		/// <summary>
		/// Добавляет нового петуха в асинхронном режиме.
		/// </summary>
		/// <returns>Асинхронно выполняющеюся задачу.</returns>
		private async Task AddRoosterAsync()
		{
			var rooster = new RoosterModel();
			if (await _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(rooster) == true)
			{
				await _editServiceClient.AddAsync(token, rooster.ToRoosterDto());
				UpdateRoostersAsync();
			}
		}

		/// <summary>
		/// Начинает сражение петухов в асинхронном режиме.
		/// </summary>
		private async Task StartRoostersFightAsync()
		{
			ShowWindow = false;
			await _uiVisualizerService.ShowAsync<FightViewModel>(SelectedRooster);
			ShowWindow = true;
			UpdateRoostersAsync();
		}
		#endregion
	}
}
