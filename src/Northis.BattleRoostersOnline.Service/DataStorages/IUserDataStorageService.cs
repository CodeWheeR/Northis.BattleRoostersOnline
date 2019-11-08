using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	/// <summary>
	/// Предоставляет доступ к механизмам хранения пользователей.
	/// </summary>
	public interface IUserDataStorageService
	{
		#region Properties
		/// <summary>
		/// Возвращает или задает данные пользователя.
		/// </summary>
		/// <value>
		/// Данные пользователя.
		/// </value>
		Dictionary<string, string> UserData { get; }

		/// <summary>
		/// Возвращает или задает данные об авторизованных пользователях.
		/// </summary>
		/// <value>
		/// Авторизованные пользователи.
		/// </value>
		Dictionary<string, string> LoggedUsers { get; }
		/// <summary>
		/// Возвращает или задает данные об игровых сессиях.
		/// </summary>
		/// <value>
		/// Игровые сессии.
		/// </value>
		Dictionary<string, Session> Sessions { get; }

		#endregion

		#region Public Methods
		/// <summary>
		/// Асинхронно сохраняет данные пользователей.
		/// </summary>
		Task SaveUserDataAsync();
		/// <summary>
		/// Загружает данные пользователей.
		/// </summary>
		void LoadUserData();
		#endregion
	}
}
