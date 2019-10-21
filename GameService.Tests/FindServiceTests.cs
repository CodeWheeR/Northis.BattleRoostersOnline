using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.GameService.Tests
{

	[TestFixture()]
	class FindServiceTests
	{
		private FindService _findService;

		[SetUp]
		public void Setup()
		{
			_findService = new FindService();
			var container = new UnityContainer();

			container.RegisterInstance(new ServicesStorage()
			{
				RoostersData = new Dictionary<string, List<RoosterDto>>(),
				UserData = new Dictionary<string, string>(),
				LoggedUsers = new Dictionary<string, string>(),
				Sessions = new Dictionary<string, Session>()
			});

			var locator = new UnityServiceLocator(container);

			ServiceLocator.SetLocatorProvider((() => locator));

		}

		[Test]
		public void FindMatchTest1()
		{
			Assert.DoesNotThrow((() => _findService.FindMatch("SomeToken", new RoosterDto())));
		}



	}
}
