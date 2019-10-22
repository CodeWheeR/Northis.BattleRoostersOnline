using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
	[TestFixture]
	public class EditServiceTests
	{
		private EditService _editor = new EditService();

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

		[Test]
		public async Task AddTest1()
		{
			await _editor.AddAsync("SomeToken", new RoosterDto());
			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count, 1);
		}

		[Test]
		public void AddTest2()
		{
			Assert.DoesNotThrowAsync(async () => await _editor.AddAsync("SomeToken", new RoosterDto()));
		}

		[Test]
		public async Task AddTest3()
		{
			RoosterDto rooster = new RoosterDto();


			await _editor.AddAsync("SomeToken", rooster);

			Assert.IsTrue(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Value[0].Equals(rooster));
		}

		[Test]
		public void LoadTest1()
		{
			lock (_editor)
			{
				Assert.DoesNotThrow(() => _editor.Load());
			}
			
		}

		[Test]
		public async Task LoadTest2()
		{
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add("SomeKey", new List<RoosterDto>
			{
				new RoosterDto(),
				new RoosterDto(),
				new RoosterDto()
			});
			lock (_editor)
			{
				Task.WaitAll(_editor.SaveAsync());
				_editor.Load();

			}
			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count, 1);
			Assert.AreEqual(ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Value.Count, 3);
		}

		[Test]
		public async Task GetUserRoostersTest1()
		{
			IEnumerable<RoosterDto> roosters = await _editor.GetUserRoostersAsync("NoFindToken");

			Assert.AreEqual(new List<RoosterDto>(), roosters.ToList());
		}

		[Test]
		public async Task GetUserRoostersTest2()
		{ 
			Assert.DoesNotThrowAsync(() => _editor.GetUserRoostersAsync("NotFindToken"));
		}

		[Test]
		public async Task SaveTest1()
		{
			Assert.DoesNotThrowAsync(() => _editor.SaveAsync());
		}

		[Test]
		public async Task SaveTest2()
		{
			string token = "First";
			RoosterDto rooster = new RoosterDto();
			ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Add(token, new List<RoosterDto>{rooster});

			await _editor.SaveAsync();
			_editor.Load();


			List<RoosterDto> list =  ServiceLocator.Current.GetInstance<ServicesStorage>()
						  .RoostersData.ElementAt(0)
						  .Value;

			bool isTrue = list[0]
				.Equals(rooster);



			Assert.AreEqual(token, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.ElementAt(0).Key);

			Assert.IsTrue(ServiceLocator.Current.GetInstance<ServicesStorage>()
										  .RoostersData.ElementAt(0)
										  .Value.ElementAt(0)
										  .Equals(rooster));
		}

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
