using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Catel;
using Catel.Data;
using Catel.ExceptionHandling;
using Catel.MVVM;
using Catel.Services;
using Northis.RoosterBattle.Models;
// ReSharper disable All

namespace Northis.RoosterBattle.ViewModels
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
		/// Сервис сохранения и загрузки петухов.
		/// </summary>
		private readonly IRoosterKeepService _roosterKeepService;
		/// <summary>
		/// Сервис работы с ошибками.
		/// </summary>
		private readonly IExceptionService _exceptionService;
		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Текущий выбранный" петух.
		/// </summary>
		public static readonly PropertyData SelectedRoosterProperty = RegisterProperty(nameof(SelectedRooster), typeof(RoosterModel));
		/// <summary>
		/// Зарегистрированное свойство петухи.
		/// </summary>
		public static readonly PropertyData RoostersProperty = RegisterProperty(nameof(Roosters), typeof(IEnumerable<RoosterModel>));

		public static readonly PropertyData TokenProperty = RegisterProperty(nameof(Token), typeof(string));
		#endregion

		#endregion

		#region Properties		

		public string Token
		{
			get => GetValue<string>(TokenProperty);
			set => SetValue(TokenProperty, value);
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
		/// Свойство, предоставляющее команду сохранения петухов.
		/// </summary>
		/// <value>
		/// Команда сохранения петухов.
		/// </value>
		public ICommand SaveRoostersCommand
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
		public ICommand InitializeViewModelCommand
		{
			get;
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
		/// Инициализирует новый объект <see cref="RoosterBrowserViewModel"/> класса.
		/// </summary>
		/// <param name="roosterKeepService">Сервис сохранения и загрузки петухов.</param>
		/// <param name="uiVisualizerService">Сервис визуализации окон приложения.</param>
		public RoosterBrowserViewModel(IRoosterKeepService roosterKeepService, IUIVisualizerService uiVisualizerService, IExceptionService exceptionService)
		{
			_roosterKeepService = roosterKeepService;
			_uiVisualizerService = uiVisualizerService;
			_exceptionService = exceptionService;
			Roosters = new ObservableCollection<RoosterModel>();
			SaveRoostersCommand = new TaskCommand(SaveExecute, () => Roosters != null);
			EditRoosterCommand = new TaskCommand(EditRoosterAsync, () => SelectedRooster != null);
			DeleteRoosterCommand = new TaskCommand(DeleteRoosterAsync, () => SelectedRooster != null);
			AddRoosterCommand = new TaskCommand(AddRoosterAsync);
			FightCommand = new TaskCommand(StartRoostersFightAsync, () => ((ObservableCollection<RoosterModel>)Roosters).Count > 1);
		}


		/// <summary>
		/// Выполняет синхронное сохранение петухов перед выходом.
		/// </summary>
		/// <returns></returns>
		private Task SaveExecute()
		{
			_roosterKeepService.SaveRoosters(Roosters);
			return Task.CompletedTask;
		}
		#endregion

		#region Protected Methods

		/// <summary>
		/// Инициализирует модель-представление.
		/// </summary>
		protected override async Task InitializeAsync()
		{
			await _uiVisualizerService.ShowDialogAsync<AuthViewModel>();

			Argument.IsNotNull(nameof(_roosterKeepService), _roosterKeepService);
			Argument.IsNotNull(nameof(_exceptionService), _exceptionService);

			IEnumerable<RoosterModel> roosters = await _exceptionService.ProcessAsync(_roosterKeepService.LoadRoostersAsync);
			
			Argument.IsNotNull(nameof(roosters), roosters);
			Roosters = new ObservableCollection<RoosterModel>(roosters);

			await base.InitializeAsync();
		}
		/// <summary>
		/// Сохраняет петухов.
		/// </summary>
		protected void SaveRoosters()
		{
			_roosterKeepService.SaveRoostersAsync(Roosters);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Открывает окно редактирования выбранного петуха в асинхронном режиме.
		/// </summary>
		/// <returns>Окно редактирования.</returns>
		private Task EditRoosterAsync()
		{
			return _uiVisualizerService.ShowDialogAsync<EditRoosterViewModel>(SelectedRooster);
		}
		/// <summary>
		/// Удаляет выбранного петуха.
		/// </summary>
		/// <returns></returns>
		private  Task DeleteRoosterAsync()
		{
			(Roosters as ObservableCollection<RoosterModel>).Remove(SelectedRooster);
			SelectedRooster = null;
			return Task.CompletedTask;
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
				(Roosters as ObservableCollection<RoosterModel>).Add(rooster);
				SelectedRooster = rooster;
			}
		}
		/// <summary>
		/// Начинает сражение петухов в асинхронном режиме.
		/// </summary>
		private async Task StartRoostersFightAsync()
		{
			await _uiVisualizerService.ShowDialogAsync<FightViewModel>(Roosters);
		}
		#endregion

		/// <summary>
		/// Closes this instance. Always called after the <see cref="M:Catel.MVVM.ViewModelBase.CancelAsync" /> of <see cref="M:Catel.MVVM.ViewModelBase.SaveAsync" /> method.
		/// </summary>
		protected override Task CloseAsync()
		{
			SaveExecute();
			return base.CloseAsync();
		}
	}
}
