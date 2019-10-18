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
			token = client.Register("Вася Пупкин", "23455654");
			if (token == "AlreadyRegistered")
			{
				token = client.Register("Вася Пупкин1", "23451231231");
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
			
			var finder = new BattleServiceClient(new InstanceContext(new BattleCallbacker()));
			finder.FindMatch(token, rooster);
			finder.CancelFinding(token);
			Console.WriteLine("Match Was Canceled");

			Console.ReadKey();
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

			public void FindedMatch(string matchToken)
			{
				Console.WriteLine("Match finded: " + matchToken);
			}
		}
	}
}
