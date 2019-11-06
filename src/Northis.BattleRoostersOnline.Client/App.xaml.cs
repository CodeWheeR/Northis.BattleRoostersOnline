using System;
using System.IO;
using System.Security;
using System.Windows;
using AutoMapper;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Newtonsoft.Json;
using NLog;
using Northis.BattleRoostersOnline.Client.Extensions;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.Views;
using LogManager = Catel.Logging.LogManager;

namespace Northis.BattleRoostersOnline.Client
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			LogManager.AddDebugListener();

			var serviceLocator = this.GetServiceLocator();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<RoosterModel, RoosterEditDto>();
			});

			serviceLocator.RegisterInstance(config.CreateMapper());


			var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
			uiVisualizerService.Register<RoosterBrowserViewModel, RoosterBrowserWindow>();
			var exceptionService = serviceLocator.ResolveType<IExceptionService>();
			exceptionService.Register<JsonReaderException>(async exception =>
			{
				var messageService = serviceLocator.ResolveType<IMessageService>();
				await messageService.ShowAsync("Ошибка чтения файла сохранения. Мои соболезнования...", "Ошибка!", MessageButton.OK, MessageImage.Error);
			});
			StartApp(uiVisualizerService);
		}




		private async void StartApp(IUIVisualizerService uiVisualizerService)
		{
			try
			{
				await uiVisualizerService.ShowDialogAsync<RoosterBrowserViewModel>();
			}
			catch (Exception e)
			{
				throw;
			}
		}

		private void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = NLog.LogManager.GetCurrentClassLogger();
			logger.Fatal(e);
			MessageBox.Show("Возникло необработанное исключение. Проверьте Log 'Fatal'");
		}
	}
}
