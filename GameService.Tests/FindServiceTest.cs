using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.GameService.Tests
{


	[TestFixture()]
	class FindServiceTest
	{
		private FindService _findService;

		[SetUp]
		public void Setup()
		{
			_findService = new FindService();

			var container = new UnityContainer();

			container.RegisterInstance(new ServicesStorage()
			{
			RoostersData	= new Dictionary<string, List<RoosterDto>>()
			{
				{
					"First", new List<RoosterDto>()
					{
						new RoosterDto()
						{
							Health = 11,
							Damage = 12
						},
						new RoosterDto()
						{
							Health = 13,
							Damage = 14
						}
					}	
				},
				{
					"Second", new List<RoosterDto>()
					{
						new RoosterDto()
						{
							Health = 25,
							Damage = 44
						},
						new RoosterDto()
						{
							Health = 78,
							Damage = 10
						},
						new RoosterDto()
						{
							Health = 14,
							Damage = 26
						}
					}
				}
			},
			LoggedUsers = new Dictionary<string, string>()
			{
				{"ssssss", "User1" },
				{"dasd1231dasdqw", "User2" }
			},
			Sessions = new Dictionary<string, Session>()
			{
				{"asdlmkad", new Session("asdlmkad") },
				{"www", new Session("www") }
			}
			});

			var locator = new UnityServiceLocator(container);

			ServiceLocator.SetLocatorProvider((() => locator));
		}

		[Test]
		public void FindMathTest1()
		{
			_findService.FindMatch("SomeUSer", new RoosterDto());



		}


	}
}
