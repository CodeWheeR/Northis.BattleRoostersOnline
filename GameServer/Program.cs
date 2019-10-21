using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Implements;

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
			var baseAddress = new Uri("http://localhost:23555/Northis.BattleRoostersOnline/");

			var selfHost = new ServiceHost(typeof(GameServicesProvider), baseAddress);

			try
			{
				selfHost.AddServiceEndpoint(typeof(IAuthenticateService), new WSHttpBinding(), "AuthenticationService");

				selfHost.AddServiceEndpoint(typeof(IEditService), new WSHttpBinding(), "EditService");

				selfHost.AddServiceEndpoint(typeof(IBattleService), new WSDualHttpBinding(), "BattleService");

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
