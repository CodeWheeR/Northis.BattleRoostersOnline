using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Contracts;

namespace GameServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Uri baseAddress = new Uri("http://localhost:23555/Northis.BattleRoostersOnline/");

			// Step 2: Create a ServiceHost instance.
			ServiceHost selfHost = new ServiceHost(typeof(GameServicesProvider), baseAddress);

			try
			{
				// Step 3: Add a service endpoint.
				selfHost.AddServiceEndpoint(typeof(IAuthenticateService), new WSHttpBinding(), "AuthenticationService");

				selfHost.AddServiceEndpoint(typeof(IEditService), new WSHttpBinding(), "EditService");

				selfHost.AddServiceEndpoint(typeof(IBattleService), new WSDualHttpBinding(), "BattleService");

				// Step 4: Enable metadata exchange.
				ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
				smb.HttpGetEnabled = true;
				selfHost.Description.Behaviors.Add(smb);

				// Step 5: Start the service.
				selfHost.Open();
				Console.WriteLine("The service is ready.");

				// Close the ServiceHost to stop the service.
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
