using System.Collections.Generic;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	/// <summary>
	/// Предоставляет доступ к механизмам хранения петухов.
	/// </summary>
	public interface IRoosterStorageService
	{
		/// <summary>
		/// Возвращает или задает данные петухов.
		/// </summary>
		/// <value>
		/// Данные петухов.
		/// </value>
		Dictionary<string, Dictionary<string, RoosterModel>> RoostersData
		{
			get;
		}

		/// <summary>
		/// Асинхронно сохраняет петухов.
		/// </summary>
		Task SaveRoostersAsync();

		/// <summary>
		/// Загружает петухов.
		/// </summary>
		void LoadRoosters();
	}
}
