using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Models;
using Northis.BattleRoostersOnline.Service.Tests;
using NUnit.Framework;

namespace Northis.BattleRoostersOnline.Service.Tests
{
    /// <summary>
    /// Тестирует работу хранилища данных.
    /// </summary>
    [TestFixture]
	public class ServicesStorageTests : ServiceModuleTests
	{
		[OneTimeSetUp]
		public void SetUp()
		{
			Setup();
		}

		/// <summary>
		/// Асинхронно проверяет количество петухов после загрузки.
		/// </summary>
		[Test]
		public async Task SaveAndLoadRoosters()
		{
			var backupRoosters = Storage.RoostersData.Count;
			var roosters = new Dictionary<string, RoosterModel>
			{
				{
					"Rooster1", new RoosterModel()
					{
						Weight = 1,
						Height = 20,
						Health = 100,
						Stamina = 100,
						Brickness = 10,
						Thickness = 10,
						Luck = 10,
						Name = "CoCoCo",
						Crest = CrestSizeType.Small,
						Color = RoosterColorType.Black,
						MaxHealth = 100,
						Token = "asdasd123",
						WinStreak = 1
					}
				},
				{
					"Rooster2", new RoosterModel()
					{
						Weight = 2,
						Height = 30,
						Health = 89,
						Stamina = 100,
						Brickness = 20,
						Thickness = 30,
						Luck = 30,
						Name = "CoCaCo",
						Crest = CrestSizeType.Big,
						Color = RoosterColorType.Red,
						MaxHealth = 120,
						Token = "asdasd1234",
						WinStreak = 10
					}
				}
			};

			Storage.RoostersData.Add("SomeKey", roosters);

			await Storage.SaveRoostersAsync();

			Storage.LoadRoosters();

			Assert.AreEqual(Storage.RoostersData.Count, backupRoosters + 1);

			Assert.IsTrue(Storage.RoostersData["SomeKey"].SequenceEqual(roosters));
		}

		/// <summary>
		/// Асинхронно проверяет корректность метода сохранения петухов на предмет исключительных ситуаций.
		/// </summary>
		[Test]
		public async Task SaveAndLoadUsers()
		{
			var users = new Dictionary<string, string>()
			{
				{
					"user1", "password1"
				},
				{
					"user2", "password2"
				}
			};

			foreach (var i in users)
			{
				await AuthenticateService.RegisterAsync(i.Key, i.Value, Mock.Of<IAuthenticateServiceCallback>());
			}

			foreach (var i in Storage.UserData)
			{
				if (users.ContainsKey(i.Key))
				{
					users[i.Key] = Storage.UserData[i.Key];
				}
			}

			await Storage.SaveUserDataAsync();

			Storage.UserData.Clear();

			Storage.LoadUserData();

			Assert.IsTrue(Storage.UserData.SequenceEqual(users));
		}
	}
}
