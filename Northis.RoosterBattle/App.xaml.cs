using System;
using System.Windows;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Newtonsoft.Json;
using Northis.RoosterBattle.Services;
using Northis.RoosterBattle.ViewModels;
using Northis.RoosterBattle.Views;

namespace Northis.RoosterBattle
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			LogManager.AddDebugListener();

			var serviceLocator = this.GetServiceLocator();
			serviceLocator.RegisterType<IRoosterKeepService, RoosterKeepService>();
			var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
			uiVisualizerService.Register<RoosterBrowserViewModel, RoosterBrowserWindow>();
			var exceptionService = serviceLocator.ResolveType<IExceptionService>();
			exceptionService.Register<JsonReaderException>(async exception =>
			{
				var messageService = serviceLocator.ResolveType<IMessageService>();
				await messageService.ShowAsync("Ошибка чтения файла сохранения. Мои соболезнования...", "Ошибка!", MessageButton.OK, MessageImage.Error);
			});

			uiVisualizerService.ShowDialogAsync<RoosterBrowserViewModel>();
		}

		private async void StartApp(IUIVisualizerService uiVisualizerService)
		{
			await uiVisualizerService.ShowDialogAsync<AuthViewModel>().ConfigureAwait(true);
			if (Current.Resources.Contains("UserToken"))
				await uiVisualizerService.ShowDialogAsync<RoosterBrowserViewModel>().ConfigureAwait(true);
		}
	}
}
