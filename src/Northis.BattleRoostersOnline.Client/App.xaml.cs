using System;
using System.IO;
using System.Security;
using System.Windows;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Newtonsoft.Json;
using NLog;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.Views;
using LogManager = Catel.Logging.LogManager;

namespace Northis.BattleRoostersOnline.Client
{
    /// <summary>
    /// Обеспечивает логику взаимодействия с App.xaml.
    /// </summary>
    public partial class App : Application
	{
        #region .ctor
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="App"/> класса.
        /// </summary>
        public App()
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			LogManager.AddDebugListener();

			var serviceLocator = this.GetServiceLocator();
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
        #endregion

        #region Private Methods        
        /// <summary>
        /// Запускает приложение.
        /// </summary>
        /// <param name="uiVisualizerService">Сервис визуализации.</param>
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
        /// <summary>
        /// Обрабатывает необработанные исключения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e"><see cref="UnhandledExceptionEventArgs"/> Экземпляр, содержащий информацию о событии.</param>
        private void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = NLog.LogManager.GetCurrentClassLogger();
			logger.Fatal(e);
			MessageBox.Show("Возникло необработанное исключение. Проверьте Log 'Fatal'");
		}
        #endregion
    }
}
