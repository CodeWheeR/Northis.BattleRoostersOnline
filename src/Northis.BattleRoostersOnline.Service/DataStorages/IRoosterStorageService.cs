using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	public interface IRoosterStorageService
	{
		/// <summary>
		/// Возвращает или задает данные петухов.
		/// </summary>
		/// <value>
		/// Данные петухов.
		/// </value>
		Dictionary<string, Dictionary<string, RoosterModel>> RoostersData { get; }
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
