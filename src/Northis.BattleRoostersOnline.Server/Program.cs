using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Implements;
using Unity;
using Unity.Wcf;

namespace Northis.BattleRoostersOnline.Server
{
	/// <summary>
	/// Класс-хост, обеспечивающий размещение службы.
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// Определяет точку входа в хост-приложение. Запускает хостинг сервиса.
		/// </summary>
		/// <param name="args">Аргументы.</param>
		private static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			string address = "";

			try
			{
				address = ConfigurationManager.AppSettings["baseAddress"];
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("IP-адрес по ключу baseAddress в разделе AppSettings файла конфигураций не найден. Завершаю работу...");
				Console.ReadKey();
				return;
			}

			if (address == "")
			{
				Console.WriteLine("IP-адрес по ключу baseAddress в разделе AppSettings файла конфигураций не найден. Завершаю работу...");
				Console.ReadKey();
				return;
			}

			var baseAddress = new Uri(address);

			var container = new UnityContainer();

			container.RegisterType<IDataStorageService, DataStorageService>();
			container.RegisterType<IEditService, EditService>();
			container.RegisterType<IBattleService, BattleService>();
			container.RegisterType<IAuthenticateService, AuthenticateService>();


			var selfHost = new UnityServiceHost(container, typeof(GameServicesProvider), baseAddress);

			try
			{
				
				var authBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
				{
					OpenTimeout = new TimeSpan(0, 0, 0, 5),
					SendTimeout = new TimeSpan(0, 0, 0, 5),
				};

				var editBinding = new WSHttpBinding(SecurityMode.None)
				{
					OpenTimeout = new TimeSpan(0, 0, 0, 5),
					SendTimeout = new TimeSpan(0, 0, 0, 5)
				};

				var battleBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
				{
					SendTimeout = new TimeSpan(0, 0, 0, 1)
				};

				selfHost.AddServiceEndpoint(typeof(IAuthenticateService), authBinding, "AuthenticationService");
				selfHost.AddServiceEndpoint(typeof(IEditService), editBinding, "EditService");
				selfHost.AddServiceEndpoint(typeof(IBattleService), battleBinding, "BattleService");

				var smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				selfHost.Description.Behaviors.Add(smb);
				selfHost.Open();
				Console.WriteLine("The service is ready.");
				Console.WriteLine("Press <Enter> to terminate the service.");
				Console.ReadLine();
				selfHost.Close();
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("An exception occurred: {0}", ce.Message);
				selfHost.Abort();
			}

			Console.ReadLine();
		}
		/// <summary>
		/// Действие при обработке Unhandled Exception.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = LogManager.GetCurrentClassLogger();
			if (e.ExceptionObject is Exception ex)
				logger.Fatal(ex);
			else
				logger.Fatal(e.ExceptionObject);

			Console.WriteLine( $"[FATAL] Возникло необработанное исключение {e.ExceptionObject.GetType()}. Проверьте логи...");
			Console.ReadLine();
			Environment.Exit(1);
		}
	}
}
