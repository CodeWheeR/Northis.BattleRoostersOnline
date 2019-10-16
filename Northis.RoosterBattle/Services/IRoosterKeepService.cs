using System.Collections.Generic;
using System.Threading.Tasks;
using Northis.RoosterBattle.Models;

namespace Northis.RoosterBattle
{
	/// <summary>
	/// Предоставляет механизмы сохранения и загрузки информации о петухах.
	/// </summary>
	interface IRoosterKeepService
	{
		/// <summary>
		/// Сохраняет петухов.
		/// </summary>
		/// <param name="roosters">The roosters.</param>
		void SaveRoosters(IEnumerable<RoosterModel> roosters);
		/// <summary>
		/// Загружает петухов.
		/// </summary>
		/// <returns>Коллекцию петухов.</returns>
		IEnumerable<RoosterModel> LoadRoosters();
		/// <summary>
		/// Ставит на выполнение метод загрузки петухов в асинхронном режиме.
		/// </summary>
		/// <returns>Задачу, типизированную коллекцией петухов.</returns>
		Task<IEnumerable<RoosterModel>> LoadRoostersAsync();
		/// <summary>
		/// Ставит на выполнение метод сохранения петухов в асинхронном режиме.
		/// </summary>
		/// <param name="roosters">The roosters.</param>
		/// <returns>Task</returns>
		Task SaveRoostersAsync(IEnumerable<RoosterModel> roosters);
	}
}
