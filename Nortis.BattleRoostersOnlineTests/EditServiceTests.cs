using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;

namespace Nortis.BattleRoostersOnlineTests
{
	/// <summary>
	/// Проверяет работу сервиса редактирования.
	/// </summary>
	[TestFixture]
	public class EditServiceTests
	{
		/// <summary>
		/// Сервис редактирования.
		/// </summary>
		private EditService _editor = new EditService();
		/// <summary>
		/// Настраивает тестовое окружение.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage
			{
				RoostersData = new Dictionary<string,List<RoosterDto>>(),
				UserData = new Dictionary<string, string>(),
				LoggedUsers = new Dictionary<string, string>(),
				Sessions = new Dictionary<string, Session>()
			});

			UnityServiceLocator locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}
		/// <summary>
		/// Проверяет корректность метода добавления нового петуха пользователя.
		/// </summary>
		[Test]
		public async Task AddTest1()
		{
			await _editor.AddAsync("SomeToken", new RoosterDto());

			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count, 1);
		}
		/// <summary>
		/// Проверяет метод на предмет выброса исключений.
		/// </summary>
		[Test]
		public void AddTest2()
		{
			Assert.DoesNotThrowAsync(async () => await _editor.AddAsync("SomeToken", new RoosterDto()));
		}
		/// <summary>
		/// Проверяет количество петухов пользователя после добавления.
		/// </summary>
		[Test]
		public async Task AddTest3()
		{
			RoosterDto rooster = new RoosterDto();

			await _editor.AddAsync("SomeToken", rooster);

			Assert.IsTrue(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Value[0].Equals(rooster));
		}
		
		/// <summary>
		/// Проверяет корректность получения петухов пользователя.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest1()
		{
			IEnumerable<RoosterDto> roosters = await _editor.GetUserRoostersAsync("NoFindToken");

			Assert.AreEqual(new List<RoosterDto>(), roosters.ToList());
		}
		/// <summary>
		/// Проверяет корректность метода получения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task GetUserRoostersTest2()
		{ 
			Assert.DoesNotThrowAsync(() => _editor.GetUserRoostersAsync("NotFindToken"));
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
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				new RoosterDto()
			});

			Assert.DoesNotThrowAsync(() => _editor.EditAsync(token, val, new RoosterDto()));
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
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add("FoundToken", new List<RoosterDto>
			{
				new RoosterDto()
			});

			Assert.DoesNotThrowAsync(() => _editor.RemoveAsync(token, val));
		}
	}
}
