using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;

namespace Nortis.BattleRoostersOnlineTests
{
	[TestFixture]
	public class ServvicesStorageTests
	{
		private ServicesStorage _servicesStorage = new ServicesStorage();

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

			UnityServiceLocator locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}
		/// <summary>
		/// Проверяет метод загрузки петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public void LoadTest1()
		{
			Assert.DoesNotThrow(() => _servicesStorage.LoadRoosters());

		}
		/// <summary>
		/// Проверяет количество петухов после загрузки.
		/// </summary>
		[Test]
		public async Task LoadTest2()
		{
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add("SomeKey", new List<RoosterDto>
			{
				new RoosterDto(),
				new RoosterDto(),
				new RoosterDto()
			});
			Task.WaitAll(_servicesStorage.SaveRoostersAsync());
			_servicesStorage.LoadRoosters();

			

			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count, 1);
			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Value.Count, 3);
		}
		/// <summary>
		/// Проверяет корректность метода сохранения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task SaveTest1()
		{
			Assert.DoesNotThrowAsync(() => _servicesStorage.SaveRoostersAsync());
		}
		/// <summary>
		/// Проверяет корректность метода сохранения петухов.
		/// </summary>
		[Test]
		public async Task SaveTest2()
		{
			string token = "First";
			RoosterDto rooster = new RoosterDto();
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add(token, new List<RoosterDto> { rooster });

			await _servicesStorage.SaveRoostersAsync();
			_servicesStorage.LoadRoosters();


			Assert.AreEqual(token, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Key);
			Assert.IsTrue(ServiceLocator.Current.GetInstance<ServicesStorage>()
										.RoostersData.ElementAt(0)
										.Value.ElementAt(0)
										.Equals(rooster));
		}
		/// <summary>
		/// Проверяет корректность работы метода сохранения данных пользователя.
		/// </summary>
		[Test]
		public async Task SaveUsersTest1()
		{
			Assert.DoesNotThrowAsync(() => _servicesStorage.SaveUserDataAsync());
		}
		/// <summary>
		/// Проверяет корректность работы метода загрузки данных пользователя.
		/// </summary>
		[Test]
		public async Task LoadUsersTest1()
		{
			Assert.DoesNotThrow(() => _servicesStorage.LoadUserData());
		}
	}
}
