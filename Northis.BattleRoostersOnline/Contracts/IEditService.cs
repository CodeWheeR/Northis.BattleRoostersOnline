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
		/// Редактирует выбранного петуха конкретного пользователя.
		/// </summary>
		/// <param name="login">Идентификатор.</param>
		/// <param name="roosterSeqNum">Порядковый номер петуха.</param>
		/// <param name="rooster">Петух.</param>
		[OperationContract]
		void Edit(string login, int roosterSeqNum, RoosterDto rooster);
		[OperationContract]
		void Add(string login, RoosterDto rooster);
		[OperationContract]
		void Remove(string token, int roosterID);
		[OperationContract]
		Task<IEnumerable<RoosterDto>> GetUserRoosters(string token);
	}
}
