﻿using System;
using System.ServiceModel;
using System.ServiceModel.Activation.Configuration;
using System.ServiceModel.Description;
using System.Threading;
using NLog;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;

namespace GameServer
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

			var baseAddress = new Uri("http://10.88.99.75:8080/Northis.BattleRoostersOnline");

			var selfHost = new ServiceHost(typeof(GameServicesProvider), baseAddress);

			try
			{
				DataStorageService.InitContainer();

				var authBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
				{
					OpenTimeout = new TimeSpan(0, 0, 0, 5),
					SendTimeout = new TimeSpan(0, 0, 0, 5),
				};

				var battleBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
				{
					SendTimeout = new TimeSpan(0, 0, 0, 1)
				};

				selfHost.AddServiceEndpoint(typeof(IAuthenticateService), authBinding, "AuthenticationService");
				selfHost.AddServiceEndpoint(typeof(IEditService), new WSHttpBinding(SecurityMode.None), "EditService");
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

		static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = LogManager.GetCurrentClassLogger();
			if (e.ExceptionObject is Exception ex)
				logger.Fatal(ex, "Unhandled Exception");
			else
				logger.Fatal(e.ExceptionObject);

			Console.WriteLine(e.ExceptionObject.ToString());
			Console.WriteLine("Press Enter to continue");
			Console.ReadLine();
			Environment.Exit(1);
		}
	}
}
