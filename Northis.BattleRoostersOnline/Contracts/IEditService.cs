using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;

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
		/// <param name="login">Идентификатор.</param>
		/// <param name="roosterSeqNum">Порядковый номер петуха.</param>
		/// <param name="rooster">Петух.</param>
		[OperationContract]
		Task EditAsync(string token, int roosterID, RoosterDto rooster);
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
		/// <param name="roosterID">Идентификатор петуха.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task RemoveAsync(string token, int roosterID);
		/// <summary>
		/// Контракт операции, ответственный за возврат петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Коллекцию петухов.</returns>
		[OperationContract]
		Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token);
	}
}
