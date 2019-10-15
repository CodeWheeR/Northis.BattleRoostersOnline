using System.Collections.Generic;
using CommonServiceLocator;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.GameService.Tests
{
	[TestFixture]
	public class AuthenticateTests
	{
		[SetUp]
		public void Setup()
		{
			var ioc = ServiceLocator.Current;
			var container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage()
			{
				UserData = new Dictionary<string, string>()
				{
					{"Вася Пупкин", "asdshka"},
					{"Петя Паравозов",  "88005553535"}
				}
			});

			var locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}

		[TestCase("Вася Пупкин", "asdshka")]
		public void Test1(string login, string password)
		{
			var s = new AuthenticateService().GenerateToken();
		}
	}
}