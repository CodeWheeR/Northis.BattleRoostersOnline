using System;
using System.ServiceModel;
using System.ServiceModel.Description;
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
			var baseAddress = new Uri("http://localhost:7777/Northis.BattleRoostersOnline");

			var selfHost = new ServiceHost(typeof(GameServicesProvider), baseAddress);

			try
			{
				DataStorageService.InitContainer();

				var authBinding = new WSDualHttpBinding()
				{
					SendTimeout = new TimeSpan(0, 0, 0, 60),
					Security = new WSDualHttpSecurity()
					{
						Mode = WSDualHttpSecurityMode.None
					}
				};

				var battleBinding = new WSDualHttpBinding()
				{
					SendTimeout = new TimeSpan(0, 0, 0, 60),
					Security = new WSDualHttpSecurity()
					{
						Mode = WSDualHttpSecurityMode.None,
					}
				};

				selfHost.AddServiceEndpoint(typeof(IAuthenticateService), authBinding, "AuthenticationService");
				selfHost.AddServiceEndpoint(typeof(IEditService), new WSHttpBinding(), "EditService");
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
	}
}
