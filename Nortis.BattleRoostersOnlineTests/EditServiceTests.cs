using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;

namespace Nortis.BattleRoostersOnlineTests
{
	/// <summary>
	/// Проверяет работу сервиса редактирования.
	/// </summary>
	[TestFixture]
	public class EditServiceTests : ServiceModuleTests
	{
		#region Methods
		#region Public
		/// <summary>
		/// Проверяет корректность метода добавления нового петуха пользователя.
		/// </summary>
		[Test]
		public async Task AddTest1()
		{
			string token = await authenticateService.RegisterAsync("Login1", "Password", callbackAuth.Object);
			//await editor.AddAsync(token, new RoosterDto());

			Assert.AreEqual(ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Count, 1);
		}
		/// <summary>
		/// Проверяет метод на предмет выброса исключений.
		/// </summary>
		[Test]
		public void AddTest2()
		{
			//Assert.DoesNotThrowAsync(async () => await editor.AddAsync("SomeToken", new RoosterDto()));
		}
		/// <summary>
		/// Проверяет количество петухов пользователя после добавления.
		/// </summary>
		[Test]
		public async Task AddTest3()
		{
			RoosterDto rooster = new RoosterDto();

			//await editor.AddAsync("SomeToken", rooster);

			Assert.IsTrue(ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.ElementAt(0).Value[0].Equals(rooster));
		}
		
		/// <summary>
		/// Проверяет корректность получения петухов пользователя.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest1()
		{
			IEnumerable<RoosterDto> roosters = await editor.GetUserRoostersAsync("NoFindToken");

			Assert.AreEqual(new List<RoosterDto>(), roosters.ToList());
		}
		/// <summary>
		/// Проверяет корректность метода получения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest2()
		{ 
			Assert.DoesNotThrowAsync(() => editor.GetUserRoostersAsync("NotFindToken"));
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
			Storage.RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				new RoosterDto()
			});
			

			//Assert.DoesNotThrowAsync(() => editor.EditAsync(token, Storage.RoostersData["FoundToken"].First(), new RoosterModel().ToRoosterDto()));
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
			Storage.RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				rooster1,
				rooster2,
				rooster3
			});

			//await editor.EditAsync("FoundToken", Storage.RoostersData["FoundToken"].First(x => x.Name == "asdshka"), editedRooster);

			Assert.IsTrue(editedRooster.Equals(Storage.RoostersData["FoundToken"].First(x => x.Name == "asdshka")));
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

			ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				rooster
			});

			//Assert.DoesNotThrowAsync(() => editor.RemoveAsync(token,rooster));
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
			ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				rooster1,
				rooster2,
				rooster3
			});

			//await editor.RemoveAsync(token, rooster1);

			Assert.AreEqual(false,Storage.RoostersData[token].Contains(rooster1));
		}




		#endregion
		#endregion
	}
}
