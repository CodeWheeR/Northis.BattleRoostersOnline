using System;
using System.Windows;
using AutoMapper;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.Views;
using AuthenticateStatus = Northis.BattleRoostersOnline.Client.GameServer.AuthenticateStatus;
using BattleStatus = Northis.BattleRoostersOnline.Client.GameServer.BattleStatus;

namespace Northis.BattleRoostersOnline.Client
{
	/// <summary>
	/// Обеспечивает логику взаимодействия с App.xaml.
	/// </summary>
	public partial class App : Application
	{
		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="App" /> класса.
		/// </summary>
		public App()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			LogManager.AddDebugListener();

			var serviceLocator = this.GetServiceLocator();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<RoosterModel, RoosterCreateDto>();
				cfg.CreateMap<AuthenticateStatus, Models.AuthenticateStatus>();
				cfg.CreateMap<BattleStatus, Models.BattleStatus>();
			});


			serviceLocator.RegisterInstance(config.CreateMapper());

			var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
			uiVisualizerService.Register<RoosterBrowserViewModel, RoosterBrowserWindow>();

			var res = serviceLocator.ResolveType<IMapper>()
									.Map<BattleStatus, Models.BattleStatus>(BattleStatus.AccessDenied);

			StartApp(uiVisualizerService);
		}
		#endregion

		#region Private Methods        
		/// <summary>
		/// Запускает приложение.
		/// </summary>
		/// <param name="uiVisualizerService">Сервис визуализации.</param>
		private async void StartApp(IUIVisualizerService uiVisualizerService)
		{
			await uiVisualizerService.ShowDialogAsync<RoosterBrowserViewModel>();
		}

		/// <summary>
		/// Обрабатывает необработанные исключения.
		/// </summary>
		/// <param name="sender">Источник события.</param>
		/// <param name="e"><see cref="UnhandledExceptionEventArgs" /> Экземпляр, содержащий информацию о событии.</param>
		private void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = NLog.LogManager.GetCurrentClassLogger();
			logger.Fatal(e.ExceptionObject);
			var messageService = this.GetServiceLocator()
									 .ResolveType<IMessageService>();
			messageService.ShowAsync(Client.Properties.Resources.StrErrorFatalError, Client.Properties.Resources.StrError, MessageButton.OK, MessageImage.Error);
		}
		#endregion
	}
}
