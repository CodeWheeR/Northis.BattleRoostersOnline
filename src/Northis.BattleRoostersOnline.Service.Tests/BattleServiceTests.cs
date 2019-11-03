using System.Linq;
using System.Threading.Tasks;
using Moq;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Contracts;
using NUnit.Framework;

namespace Northis.BattleRoostersOnline.Service.Tests
{
	/// <summary>
	/// Тестирует сервис проведения битвы.
	/// </summary>
	[TestFixture]
	public class BattleServiceTests : ServiceModuleTests
	{
        #region Fields
        private Mock<IBattleServiceCallback> findMatchMock;
		private string token;
        #endregion

        #region Test Methods
        /// <summary>
        /// Асинхронно проверяет корректность работы метода поиска матча.
        /// </summary>
        [Test]
		public async Task UserWasNotFound()
		{
			var findMatchMock = new Mock<IBattleServiceCallback>();
			var token = "";
			findMatchMock.Setup(s => s.FindedMatch(It.IsAny<string>()))
						 .Callback<string>(d => token = d);

			BattleService.FindMatchAsync("someToken", "someToken", findMatchMock.Object);
			BattleService.FindMatchAsync("someToken", "someToken", findMatchMock.Object);

			while (token == "");

			Assert.AreEqual(token, "User was not found");
		}

		/// <summary>
		/// Асинхронно проверяет корректность работы метода поиска матча.
		/// </summary>
		[Test]
		public async Task RoosterWasNotFound()
		{
			var findMatchMock = new Mock<IBattleServiceCallback>();
			var token = "";
			findMatchMock.Setup(s => s.FindedMatch(It.IsAny<string>()))
						 .Callback<string>(d => token = d);

			var userToken = await AuthenticateService.RegisterAsync("asdshka", "asdshka", Mock.Of<IAuthenticateServiceCallback>());

			BattleService.FindMatchAsync(userToken, "someToken", findMatchMock.Object);
			BattleService.FindMatchAsync(userToken, "someToken", findMatchMock.Object);

			while (token == "");

			Assert.AreEqual(token, "Rooster was not found");
		}

		/// <summary>
		/// Асинхронно проверяет корректность работы метода поиска матча.
		/// </summary>
		[Test]
		public async Task SameLogins()
		{
			var findMatchMock = new Mock<IBattleServiceCallback>();
			var token = "";
			findMatchMock.Setup(s => s.FindedMatch(It.IsAny<string>()))
						 .Callback<string>(d => token = d);

			var userToken = await AuthenticateService.RegisterAsync("asdshka", "asdshka", Mock.Of<IAuthenticateServiceCallback>());
			await Editor.AddAsync(userToken, new RoosterEditDto()
			{

			});

			var roosterToken = Storage.RoostersData["asdshka"]
									  .First()
									  .Key;

			BattleService.FindMatchAsync(userToken, roosterToken, Mock.Of<IBattleServiceCallback>());
			BattleService.FindMatchAsync(userToken, roosterToken, findMatchMock.Object);

			while (token == "") ;

			Assert.AreEqual(token, "SameLogins");
		}

		/// <summary>
		/// Асинхронно проверяет корректность работы метода поиска матча.
		/// </summary>
		[Test]
		public async Task FindMatch()
		{
			var findMatchMock = new Mock<IBattleServiceCallback>();
			var token = "";
			findMatchMock.Setup(s => s.FindedMatch(It.IsAny<string>()))
						 .Callback<string>(d => token = d);

			var userToken = await AuthenticateService.RegisterAsync("asdshka", "asdshka", Mock.Of<IAuthenticateServiceCallback>());
			await Editor.AddAsync(userToken, new RoosterEditDto()
			{

			});

			var roosterToken = Storage.RoostersData["asdshka"]
									  .First()
									  .Key;

			var userToken2 = await AuthenticateService.RegisterAsync("asdshka2", "asdshka", Mock.Of<IAuthenticateServiceCallback>());
			await Editor.AddAsync(userToken2, new RoosterEditDto()
			{

			});

			var roosterToken2 = Storage.RoostersData["asdshka2"]
									  .First()
									  .Key;

			BattleService.FindMatchAsync(userToken, roosterToken, findMatchMock.Object);
			BattleService.FindMatchAsync(userToken2, roosterToken2, findMatchMock.Object);

			while (token == "") ;

			Assert.IsTrue(token != "" && token != "User was not found" && token != "Rooster was not found");
		}
		#endregion
	}
}
