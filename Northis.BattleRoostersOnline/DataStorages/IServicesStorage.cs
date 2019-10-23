using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.DataStorages
{
	public interface IServicesStorage
	{
		/// <summary>
		/// Возвращает или задает данные пользователя.
		/// </summary>
		/// <value>
		/// Данные пользователя.
		/// </value>
		Dictionary<string, string> UserData{ get; }
		/// <summary>
		/// Возвращает или задает данные петухов.
		/// </summary>
		/// <value>
		/// Данные петухов.
		/// </value>
		Dictionary<string, List<RoosterDto>> RoostersData{ get; }
		/// <summary>
		/// Возвращает или задает данные об авторизированных пользователях.
		/// </summary>
		/// <value>
		/// Авторизированные пользователи.
		/// </value>
		Dictionary<string, string> LoggedUsers{ get; }
		/// <summary>
		/// Возвращает или задает данные об игровых сессиях.
		/// </summary>
		/// <value>
		/// Игровые сессии.
		/// </value>
		Dictionary<string, Session> Sessions{ get; }
		/// <summary>
		/// Асинхронно сохраняет петухов.
		/// </summary>
		Task SaveRoostersAsync();
		/// <summary>
		/// Загружает петухов.
		/// </summary>
		void LoadRoosters();
		/// <summary>
		/// Асинхронно сохраняет данные пользователей.
		/// </summary>
		Task SaveUserDataAsync();
		/// <summary>
		/// Загружает данные пользователей.
		/// </summary>
		void LoadUserData();

	}
}
