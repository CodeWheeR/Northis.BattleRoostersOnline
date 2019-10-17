using System;
using System.ServiceModel;
using TestGameClient.Game;

namespace TestGameClient
{
	class Program
	{
		static void Main(string[] args)
		{
			MainTask();
		}

		public async static void MainTask()
		{
			var client = new AuthenticateServiceClient();
			var token = client.Register("Вася Пупкин", "2345");
			if (token == "AlreadyRegistered")
			{
				token = client.Register("Вася Пупкин1", "2345");
			}

			Console.WriteLine(token);

			var rooster = new RoosterDto()
			{
				Health = 1,
				Height = 2
			};

			var finder = new FindServiceClient(new InstanceContext(new Callbacker()));
			finder.FindMatch(token, rooster);

			Console.ReadKey();
		}

		public class Callbacker : IFindServiceCallback
		{
			public void FindedMatch(string token)
			{
				Console.WriteLine("MatchWasFinded: " + token);
			}
		}
	}
}
