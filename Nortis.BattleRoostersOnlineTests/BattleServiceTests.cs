using System.Collections.Generic;
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
	/// <summary>
	/// Тестирует сервис проведения битвы.
	/// </summary>
	[TestFixture]
	public class BattleServiceTests
	{
		/// <summary>
		/// Сервис проведения битвы.
		/// </summary>
		private BattleService _battleService = new BattleService();
		/// <summary>
		/// Настраивает тестовое окружение.
		/// </summary>
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
		/// <summary>
		/// Проверяет корректность работы метода поиска матча.
		/// </summary>
		[Test]
		public async Task FindMatchTest()
		{
			var callback = new Mock<IBattleServiceCallback>();

			Assert.DoesNotThrow((() => _battleService.FindMatch("SomeToken", new RoosterDto(), callback.Object)));
		}
		/// <summary>
		/// Проверяет корректность работы метода отмены матча.
		/// </summary>
		[Test]
		public async Task CancelFightTest()
		{
			var callback = new Mock<IBattleServiceCallback>();

			Session session = new Session("SomeToken");

			session.RegisterFighter("SomeToken", new RoosterDto(), callback.Object);
			ServiceLocator.Current.GetInstance<ServicesStorage>().Sessions.Add("SomeToken", session);
			Assert.DoesNotThrow((() => _battleService.CancelFinding("SomeToken")));
		}
	}
}
