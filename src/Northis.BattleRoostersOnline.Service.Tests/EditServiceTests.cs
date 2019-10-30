using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
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
		#region Test Methods
		#region Public
		/// <summary>
		/// Проверяет корректность метода добавления нового петуха пользователя.
		/// </summary>
		[Test]
		public async Task AddTest1()
		{
			string token = await AuthenticateService.RegisterAsync("Login1", "Password", CallbackAuth.Object);
			await Editor.AddAsync(token, new RoosterDto());

			Assert.AreEqual(Storage.RoostersData.Count, 1);
		}
		/// <summary>
		/// Проверяет метод на предмет выброса исключений.
		/// </summary>
		[Test]
		public void AddTest2()
		{
			Assert.DoesNotThrowAsync( () => Editor.AddAsync("SomeToken", new RoosterDto()));
		}
		
		/// <summary>
		/// Проверяет корректность получения петухов пользователя.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest1()
		{
			IEnumerable<RoosterDto> roosters = await Editor.GetUserRoostersAsync("NoFindToken");

			Assert.AreEqual(new List<RoosterDto>(), roosters.ToList());
		}
		/// <summary>
		/// Проверяет корректность метода получения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest2()
		{ 
			Assert.DoesNotThrowAsync(() => Editor.GetUserRoostersAsync("NotFindToken"));
		}
		/// <summary>
		/// Проверяет работу метода редактирования с недопустимыми параметрами.
		/// </summary>
		/// <param name="token">Токен.</param>
		[TestCase("NotFoundToken")]
		[TestCase("NotFoundToken")]
		[TestCase("NotFoundToken")]
		public async Task EditTest1(string token)
		{
			Storage.RoostersData.Add("FoundToken", new Dictionary<string, RoosterDto>()
			{
				{
					"some", new RoosterDto()
				}
			});

			//Assert.DoesNotThrowAsync(() => editor.EditAsync(token, "some", new RoosterModel().ToRoosterDto()));
		}
		/// <summary>
		/// Проверяет корректность редактирования нужного петуха.
		/// </summary>
		public async Task EditTest2()
		{
			RoosterDto editedRooster = new RoosterDto()
			{
				Name = "asdshka",
				Height = 50
			};
			RoosterDto rooster1 = new RoosterDto()
			{
				Name = "asdshka",
				Height = 20
			};
			RoosterDto rooster2 = new RoosterDto();
			RoosterDto rooster3 = new RoosterDto();
			Storage.RoostersData.Add("FoundToken", new Dictionary<string, RoosterDto>
			{
				{ "Rooster1",rooster1},
				{ "Rooster2",rooster2},
				{ "Rooster3",rooster3}
			});

			await Editor.EditAsync("FoundToken","Rooster2" , editedRooster);

			Assert.IsTrue(editedRooster.Equals(Storage.RoostersData["FoundToken"]["Rooster2"]));
		}
		/// <summary>
		/// Проверяет работу метода редактирования с недопустимыми параметрами.
		/// </summary>
		/// <param name="token">The token.</param>
		[TestCase("NotFoundToken")]
		[TestCase("NotFoundToken")]
		[TestCase("NotFoundToken")]
		public async Task RemoveTest1(string token)
		{
			RoosterDto rooster = new RoosterDto();

			Storage.RoostersData.Add("FoundToken", new Dictionary<string, RoosterDto>
			{
				{"Rooster1", rooster}
			});

			Assert.DoesNotThrowAsync(() => Editor.RemoveAsync(token,rooster.Token));
		}
		/// <summary>
		/// Проверяет корректность удаления нужного петуха.
		/// </summary>
		/// <param name="token">The token.</param>
		public async Task RemoveTest2(string token)
		{
			RoosterDto rooster1 = new RoosterDto();
			RoosterDto rooster2 = new RoosterDto();
			RoosterDto rooster3 = new RoosterDto();
			Storage.RoostersData.Add("FoundToken", new Dictionary<string, RoosterDto>
			{
				{ "Rooster1",rooster1},
				{ "Rooster2",rooster2},
				{ "Rooster3",rooster3}
			});

			await Editor.RemoveAsync(token, "Rooster1");

			Assert.AreEqual(false,Storage.RoostersData["FoundToken"].ContainsKey("Rooster1"));
		}

		#endregion
		#endregion
	}
}
