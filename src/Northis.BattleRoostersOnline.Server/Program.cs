using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using AutoMapper;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Implements;
using Unity;
using Unity.Wcf;
using AutoMapper;
using AutoMapper.Mappers;
using GameServer.Properties;
using NLog.Targets;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Models;
using Unity.Lifetime;

namespace Northis.BattleRoostersOnline.Server
{
	/// <summary>
	/// Обеспечивает механизмы размещения службы.
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// Определяет точку входа в хост-приложение. Запускает хостинг сервиса.
		/// </summary>
		/// <param name="args">Аргументы.</param>
		private static void Main(string[] args)
		{
			Logger logger = LogManager.GetLogger("ServerLogger");

			UnityServiceHost selfHost = null;

			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			string address = "";

			try
			{
				address = ConfigurationManager.AppSettings["baseAddress"];
			}
			catch (ConfigurationErrorsException)
			{
				logger.Error(Resources.StrErrorIPAddressNotFound);
				Console.ReadKey();
				return;
			}

			if (address == "")
			{
				logger.Error(Resources.StrErrorIPAddressNotFound);
				Console.ReadKey();
				return;
			}

			var baseAddress = new Uri(address);
			var container = new UnityContainer();
			var config = new MapperConfiguration(cfg => cfg.CreateMap<RoosterModel, RoosterDto>().IgnoreAllSourcePropertiesWithAnInaccessibleSetter());
			var mapper = config.CreateMapper();

			container.RegisterInstance(mapper);
			container.RegisterType<IDataStorageService, DataStorageService>(new ContainerControlledLifetimeManager());
			var dataStorageService = container.Resolve<IDataStorageService>();
			StatisticsPublisher.GetInstance(dataStorageService);

			container.RegisterType<IEditService, EditService>(new ContainerControlledLifetimeManager());
			container.RegisterType<IBattleService, BattleService>(new ContainerControlledLifetimeManager());
			container.RegisterType<IAuthenticateService, AuthenticateService>(new ContainerControlledLifetimeManager());

			selfHost = new UnityServiceHost(container, typeof(GameServicesProvider), baseAddress);

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
				logger.Info(Resources.StrInfoServiceReady);

				while (Console.ReadKey() != new ConsoleKeyInfo((char) 13, ConsoleKey.Enter, false, false, false))
				{

				}

				StatisticsPublisher.GetInstance().SendServerStopMessage("Сервер закрыт");
				selfHost.Close();
			}
			catch (CommunicationException ce)
			{
				logger.Error(Resources.StrFrmErrorCommunicationException, ce.Message);
				selfHost.Abort();
			}
		}
		/// <summary>
		/// Действие при обработке Unhandled Exception.
		/// </summary>
		/// <param name="sender">Отправитель.</param>
		/// <param name="e">Событие.</param>
		static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
		{
			var logger = LogManager.GetCurrentClassLogger();
			if (e.ExceptionObject is Exception ex)
				logger.Fatal(ex);
			else
				logger.Fatal(e.ExceptionObject);

			Console.ReadLine();
			Environment.Exit(1);
		}
	}
}
