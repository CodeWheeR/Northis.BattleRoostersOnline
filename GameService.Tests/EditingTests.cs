using System.Collections.Generic;
using CommonServiceLocator;
using DataTransferObjects;
using NUnit.Framework;
using Northis.BattleRoostersOnline.Models;
using Unity;
using Unity.ServiceLocation;
using Northis.BattleRoostersOnline.Implements;

namespace Northis.BattleRoostersOnline.GameService.Tests
{
	[TestFixture]
	class EditingTests
	{
		private EditService _editService;

		[SetUp]
		public void Setup()
		{
			_editService = new EditService();
			var container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage()
			{
				RoostersData = new Dictionary<string, List<RoosterDto>>()
				{
					{
						"aaa", new List<RoosterDto>
						{
							new RoosterDto()
							{
								Brickness = 12,
								ColorDto = RoosterColorDto.Black
							},
							new RoosterDto()
							{
								Brickness = 15,
								ColorDto = RoosterColorDto.Red
							}
						}
					},
					{
						"Her", new List<RoosterDto>
						{
							new RoosterDto()
							{
								Brickness = 12,
								ColorDto = RoosterColorDto.Black
							},
							new RoosterDto()
							{
								Brickness = 15,
								ColorDto = RoosterColorDto.Red
							}
						}
					}
				}
			});
			var locator = new UnityServiceLocator(container);

			ServiceLocator.SetLocatorProvider(() => locator);
		}


		[Test]
		public void SaveTest()
		{
			Assert.DoesNotThrow(() => _editService.SaveAsync());
		}

		[Test]
		public void LoadTest()
		{
			Assert.DoesNotThrow(() =>
			{
				_editService.SaveAsync();
				_editService.Load();
			});
		}

		[Test]
		public void AddTest1()
		{
			_editService.Add("NewID",new RoosterDto());

			Assert.AreEqual(3,ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count);
		}

		[Test]
		public void AddTest2()
		{
			_editService.Add("aaa", new RoosterDto());

			Assert.AreEqual(2, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count);
			Assert.AreEqual(3, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData["aaa"].Count);
		}

		[Test]
		public void RemoveTest1()
		{
			_editService.RemoveAsync("aaa", 0);

			Assert.AreEqual(1, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData["aaa"].Count);
		}

		[Test]
		public void RemoveTest2()
		{
			_editService.RemoveAsync("asdkjasdajksdolasjdklasjdkasjd", 2222222);

			Assert.AreEqual(2, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count);
		}

		[TestCase("aaa", 2222222)]
		[TestCase("aaa", -1)]
		public void RemoveTest3(string key, int roosterSeqNum)
		{
			_editService.RemoveAsync(key, roosterSeqNum);

			Assert.AreEqual(2, ServiceLocator.Current.GetInstance<ServicesStorage>().RoostersData.Count);
		}

		[Test]
		public void EditTest1()
		{
			Assert.DoesNotThrow(() => _editService.EditAsync("aaa",0, new RoosterDto()));
		}

		[TestCase("ccccccc", 0)]
		[TestCase("ааа", -100)]
		[TestCase("ааа", 25)]
		public void EditTest1(string key, int roosterSeqNum)
		{
			Assert.DoesNotThrow(() => _editService.EditAsync("aaa", 0, new RoosterDto()));
		}


	}
}
