using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт, отвечающий за работу с петухами.
	/// </summary>
	[ServiceContract]
	public interface IEditService
	{
		/// <summary>
		/// Контракт операции, ответственный за редактирование выбранного петуха конкретного пользователя.
		/// </summary>
		/// <param name="token">Идентификатор.</param>
		/// <param name="editRooster">Редактируемый петух.</param>
		/// <param name="rooster">Петух.</param>
		[OperationContract]
		Task EditAsync(string token, RoosterModel editRooster, RoosterDto rooster);

		/// <summary>
		/// Контракт операции, ответственный за добавление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task AddAsync(string token, RoosterDto rooster);

		/// <summary>
		/// Контракт операции, ответственный за удаление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Удаляемый петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task RemoveAsync(string token, RoosterModel rooster);

		/// <summary>
		/// Контракт операции, ответственный за возврат петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Коллекцию петухов.</returns>
		[OperationContract]
		Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token);
	}
}
