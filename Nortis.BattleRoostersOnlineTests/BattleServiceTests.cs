﻿using System.Threading.Tasks;
using DataTransferObjects;
using NUnit.Framework;

namespace Nortis.BattleRoostersOnlineTests
{
	/// <summary>
	/// Тестирует сервис проведения битвы.
	/// </summary>
	[TestFixture]
	public class BattleServiceTests : ServiceModuleTests
	{
		#region Methods
		#region Public
		/// <summary>
		/// Проверяет корректность работы метода поиска матча.
		/// </summary>
		[Test]
		public async Task FindMatchTest()
		{
			//Assert.DoesNotThrowAsync((() =>  battleService.FindMatchAsync("SomeToken", new RoosterDto(), callbackBattle.Object)));
		}
		/// <summary>
		/// Проверяет корректность работы метода отмены матча.
		/// </summary>
		[Test]
		public async Task CancelFightTest()
		{
			string token = await authenticateService.RegisterAsync("Login1", "Password", callbackAuth.Object);
			 battleService.FindMatchAsync(token, new RoosterDto(), callbackBattle.Object);
			Assert.DoesNotThrow((() => battleService.CancelFinding(token)));
		}
		#endregion
		#endregion
	}
}
