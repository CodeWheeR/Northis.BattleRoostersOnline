using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.Contracts
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
		/// <param name="sourceRooster">Изменяемый петух.</param>
		/// <param name="editRooster">Редактированный петух.</param>
		[OperationContract]
		Task<bool> EditAsync(string token, string sourceRoosterToken, RoosterEditDto editRooster);

		/// <summary>
		/// Контракт операции, ответственный за добавление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task<bool> AddAsync(string token, RoosterEditDto rooster);

		/// <summary>
		/// Контракт операции, ответственный за удаление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Удаляемый петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task<bool> RemoveAsync(string token, string deleteRoosterToken);

		/// <summary>
		/// Контракт операции, ответственный за возврат петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Коллекцию петухов.</returns>
		[OperationContract]
		Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token);
	}
}
