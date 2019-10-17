using System;
using System.ServiceModel;
using TestGameClient.Game;

namespace TestGameClient
{
	class Program
	{
		private static string token;

		static void Main(string[] args)
		{
			MainTask();
		}

		public async static void MainTask()
		{
			var client = new AuthenticateServiceClient();
			token = client.Register("Вася Пупкин", "2345");
			if (token == "AlreadyRegistered")
			{
				token = client.Register("Вася Пупкин1", "2345");
			}

			Console.WriteLine(token);

			var rooster = new RoosterDto()
			{
				Health = 100,
				Weight = 8,
				Luck = 30,
				Brickness = 30,
				Name = "Бульбулятор"
			};

			var finder = new FindServiceClient(new InstanceContext(new Callbacker()));
			finder.FindMatch(token, rooster);



			Console.ReadKey();
		}

		public class Callbacker : IFindServiceCallback
		{
			public void FindedMatch(string matchToken)
			{
				Console.WriteLine("MatchWasFinded: " + matchToken);
				var start = new BattleServiceClient(new InstanceContext(new BattleCallbacker()));
				start.StartBattle(token, matchToken);
			}
		}

		public class BattleCallbacker : IBattleServiceCallback
		{
			public void GetRoosterStatus(RoosterDto myRooster, RoosterDto enemyRooster)
			{
				Console.WriteLine();
				Console.WriteLine($"Мой петушок: хп - {myRooster.Health}");
				Console.WriteLine($"Вражеский петушок: хп - {enemyRooster.Health}");
			}

			public void GetBattleMessage(string message)
			{
				Console.WriteLine('\n'+message+"\n");
			}

			public void GetStartSign()
			{
				Console.WriteLine('\n' + "Бой начался" + '\n');
			}
		}
	}
}
