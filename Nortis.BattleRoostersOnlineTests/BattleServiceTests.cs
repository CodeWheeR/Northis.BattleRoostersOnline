using System.Threading.Tasks;
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
		
		#endregion
		#endregion
	}
}
