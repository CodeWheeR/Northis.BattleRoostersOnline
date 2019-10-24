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
			await editor.AddAsync(token, new RoosterDto());

			Assert.AreEqual(ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Count, 1);
		}
		/// <summary>
		/// Проверяет метод на предмет выброса исключений.
		/// </summary>
		[Test]
		public void AddTest2()
		{
			Assert.DoesNotThrowAsync(async () => await editor.AddAsync("SomeToken", new RoosterDto()));
		}
		/// <summary>
		/// Проверяет количество петухов пользователя после добавления.
		/// </summary>
		[Test]
		public async Task AddTest3()
		{
			RoosterDto rooster = new RoosterDto();

			await editor.AddAsync("SomeToken", rooster);

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
		/// <param name="val">Значение.</param>
		[TestCase("NotFoundToken", -99)]
		[TestCase("NotFoundToken", 1000)]
		[TestCase("NotFoundToken", 0)]
		public async Task EditTest(string token, int val)
		{
			ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				new RoosterDto()
			});

			Assert.DoesNotThrowAsync(() => editor.EditAsync(token, val, new RoosterDto()));
		}
		/// <summary>
		/// Проверяет работу метода редактирования с недопустимыми параметрами.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="val">The value.</param>
		[TestCase("NotFoundToken", -1)]
		[TestCase("NotFoundToken", 2)]
		[TestCase("NotFoundToken", 1000)]
		public async Task RemoveTest(string token, int val)
		{
			ServiceLocator.Current.GetInstance<DataStorageService>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				new RoosterDto()
			});

			Assert.DoesNotThrowAsync(() => editor.RemoveAsync(token, val));
		}
		#endregion
		#endregion
	}
}
