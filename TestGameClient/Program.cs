using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGameClient.AuthService;

namespace TestGameClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new AuthenticateServiceClient();
			client.Open();
			var token = client.Register("Вася Пупкин", "2345");
			Console.WriteLine(client.LogIn("Вася Пупкин", "2345"));

			Console.WriteLine(client.LogOut(token));
			Console.ReadKey();
		}
	}
}
