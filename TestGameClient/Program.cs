using System;
using TestGameClient.Game;

namespace TestGameClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new AuthenticateServiceClient();

			var token = client.Register("Вася Пупкин", "2345");
			Console.WriteLine(client.LogIn("Вася Пупкин", "2345"));
			Console.WriteLine(client.LogOut(token));



			Console.ReadKey();
		}
	}
}
