using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;
using Moq;

namespace Nortis.BattleRoostersOnlineTests
{
	[TestFixture]
	public class BaseServiceTests
	{
		private BattleService _battleService = new BattleService();


		[SetUp]
		public void Setup()
		{
			UnityContainer container = new UnityContainer();

			container.RegisterInstance(new ServicesStorage
			{
				RoostersData = new Dictionary<string, List<RoosterDto>>(),
				UserData = new Dictionary<string, string>(),
				LoggedUsers = new Dictionary<string, string>(),
				Sessions = new Dictionary<string, Session>()
			});

			container.RegisterType<IBattleServiceCallback>();


			UnityServiceLocator locator = new UnityServiceLocator(container);

			ServiceLocator.SetLocatorProvider(() => locator);

		}

		[Test]
		public async Task FindMatchTest()
		{
			var callback = new Mock<IBattleServiceCallback>();
			Assert.DoesNotThrow((() => _battleService.FindMatch("SomeToken", new RoosterDto(), callback.Object)));
		}

		[Test]
		public async Task CancelFightTest()
		{
			var callback = new Mock<IBattleServiceCallback>();

			Session session = new Session("SomeToken");

			session.RegisterFighter("SomeToken", new RoosterDto(), callback.Object);
			ServiceLocator.Current.GetInstance<ServicesStorage>().Sessions.Add("SomeToken", session);
			Assert.DoesNotThrow((() => _battleService.CancelFinding("SomeToken")));
		}

		[Test]
		public async Task StartFightTest()
		{
			var callback = new Mock<IBattleServiceCallback>();

			ServiceLocator.Current.GetInstance<ServicesStorage>().Sessions.Add("SomeToken", new Session("SomeToken"));
			Session session = new Session("SomeToken");
			session.RegisterFighter("SomeToken", new RoosterDto(), callback.Object);
			session.RegisterFighter("AnotherToken", new RoosterDto(), callback.Object);


			Assert.DoesNotThrowAsync((() => _battleService.StartBattle("SomeToken", "SomeToken")));
		}


	}
}
