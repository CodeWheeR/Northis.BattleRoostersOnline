﻿using System.Collections.Generic;
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
	public class ServicesStorageTests : ServiceModuleTests
	{
		private DataStorageService _dataStorageService = new DataStorageService();

		/// <summary>
		/// Проверяет метод загрузки петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public void LoadTest1()
		{
			Assert.DoesNotThrow(() => _dataStorageService.LoadRoosters());

		}
		/// <summary>
		/// Проверяет количество петухов после загрузки.
		/// </summary>
		[Test]
		public async Task LoadTest2()
		{
			var backupRoosters = Storage.RoostersData.Count;
			Storage.RoostersData.Add("SomeKey", new List<RoosterDto>
			{
				new RoosterDto(),
				new RoosterDto(),
				new RoosterDto()
			});

			await _dataStorageService.SaveRoostersAsync();
			_dataStorageService.LoadRoosters();

			Assert.AreEqual(Storage.RoostersData.Count, backupRoosters + 1);
			Assert.AreEqual(Storage.RoostersData["SomeKey"].Count, 3);
		}
		/// <summary>
		/// Проверяет корректность метода сохранения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task SaveTest1()
		{
			Assert.DoesNotThrowAsync(() => _dataStorageService.SaveRoostersAsync());
		}
		/// <summary>
		/// Проверяет корректность метода сохранения петухов.
		/// </summary>
		[Test]
		public async Task SaveTest2()
		{
			string token = "First";
			RoosterDto rooster = new RoosterDto();
			Storage.RoostersData.Add(token, new List<RoosterDto> { rooster });

			await _dataStorageService.SaveRoostersAsync();
			_dataStorageService.LoadRoosters();


			Assert.AreEqual(token, Storage.RoostersData.ElementAt(0).Key);
			Assert.IsTrue(Storage.RoostersData.ElementAt(0)
										.Value.ElementAt(0)
										.Equals(rooster));
		}
		/// <summary>
		/// Проверяет корректность работы метода сохранения данных пользователя.
		/// </summary>
		[Test]
		public async Task SaveUsersTest1()
		{
			Assert.DoesNotThrowAsync(() => _dataStorageService.SaveUserDataAsync());
		}
		/// <summary>
		/// Проверяет корректность работы метода загрузки данных пользователя.
		/// </summary>
		[Test]
		public async Task LoadUsersTest1()
		{
			Assert.DoesNotThrow(() => _dataStorageService.LoadUserData());
		}
	}
}