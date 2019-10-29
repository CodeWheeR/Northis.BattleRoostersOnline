﻿using System;
using System.IO;
using System.Security;
using System.Windows;
using Catel.ExceptionHandling;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Newtonsoft.Json;
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
			using (StreamWriter writer = new StreamWriter("FATAL.txt", true))
			{
				writer.WriteLine(e.ExceptionObject);
			}
			MessageBox.Show("Возникло необработанное исключение. Проверьте Log 'Fatal.txt'");
		}
	}
}
