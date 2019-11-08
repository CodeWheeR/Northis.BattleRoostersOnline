using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Models;
using NUnit.Framework;

namespace Northis.BattleRoostersOnline.Service.Tests
{
	/// <summary>
	/// Проверяет работу сервиса редактирования.
	/// </summary>
	[TestFixture]
	public class EditServiceTests : ServiceModuleTests
	{
		/// <summary>
		/// Инициализует среду перед выполнением тестов.
		/// </summary>
		[OneTimeSetUp]
		public void SetUp()
		{
			Setup();
		}

		/// <summary>
		/// Асинхронно проверяет работу метода редактирования с недопустимыми параметрами.
		/// </summary>
		/// <param name="token">Токен.</param>
		[TestCase("NotFoundToken")]
		public async Task RemoveTest(string token)
		{
			var rooster = new RoosterModel();

			Storage.RoostersData.Add(token,
									 new Dictionary<string, RoosterModel>
									 {
										 {
											 "Rooster1", rooster
										 }
									 });

			Assert.DoesNotThrowAsync(() => Editor.RemoveAsync(token, rooster.Token));
		}

		/// <summary>
		/// Асинхронно проверяет корректность метода добавления нового петуха пользователя.
		/// </summary>
		[Test]
		public async Task AddTest1()
		{
			var token = await AuthenticateService.RegisterAsync("Login1", "Password", CallbackAuth.Object);

			await Editor.AddAsync(token, new RoosterCreateDto("asdshka", RoosterColorType.Black));

			Assert.AreEqual(1, Storage.RoostersData.Count);
		}

		/// <summary>
		/// Проверяет метод на предмет выброса исключений.
		/// </summary>
		[Test]
		public void AddTest2()
		{
			Assert.DoesNotThrowAsync(() => Editor.AddAsync("SomeToken", new RoosterCreateDto("", RoosterColorType.Black)));
		}

		/// <summary>
		/// Асинхронно проверяет корректность редактирования нужного петуха.
		/// </summary>
		[Test]
		public async Task EditTest()
		{
			var token = await AuthenticateService.RegisterAsync("NewUser", "asdasd123", CallbackAuth.Object);

			var editedRooster = new RoosterEditDto("asdshka", 0, 40, 0, 0, 0, CrestSizeType.Small, RoosterColorType.Black);
			var rooster1 = new RoosterEditDto("asdshka", 0, 40, 0, 0, 0, CrestSizeType.Small, RoosterColorType.Black);
			var rooster2 = new RoosterEditDto("", 0, 0, 0, 0, 0, CrestSizeType.Small, RoosterColorType.Black);
			var rooster3 = new RoosterEditDto("", 0, 0, 0, 0, 0, CrestSizeType.Small, RoosterColorType.Black);
			Storage.RoostersData.Add("NewUser",
									 new Dictionary<string, RoosterModel>
									 {
										 {
											 "Rooster1", new RoosterModel(rooster1)
										 },
										 {
											 "Rooster2", new RoosterModel(rooster2)
										 },
										 {
											 "Rooster3", new RoosterModel(rooster3)
										 }
									 });

			await Editor.EditAsync(token, "Rooster2", editedRooster);

			var editCheckRooster = new RoosterModel(editedRooster);

			Assert.AreEqual(editCheckRooster.Height,
							Storage.RoostersData["NewUser"]["Rooster2"]
								   .Height);
		}

		/// <summary>
		/// Асинхронно проверяет корректность получения петухов пользователя.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest1()
		{
			var roosters = await Editor.GetUserRoostersAsync("NoFindToken");

			Assert.AreEqual(new List<RoosterDto>(), roosters.ToList());
		}

		/// <summary>
		/// Асинхронно проверяет корректность метода получения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest2()
		{
			Assert.DoesNotThrowAsync(() => Editor.GetUserRoostersAsync("NotFindToken"));
		}
	}
}
