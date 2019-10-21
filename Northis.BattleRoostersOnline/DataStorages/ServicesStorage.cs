using System;
using System.Collections.Generic;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Models
{
	/// <summary>
	/// Класс, инкапсулирующий в себе данные о пользователях, петухах, авторизированных пользователях, игровых сессиях.
	/// </summary>
	[Serializable]
	public class ServicesStorage
	{
		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ServicesStorage" /> класса.
		/// </summary>
		public ServicesStorage()
		{
			UserData = new Dictionary<string, string>();
			RoostersData = new Dictionary<string, List<RoosterDto>>();
			LoggedUsers = new Dictionary<string, string>();
			Sessions = new Dictionary<string, Session>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает или задает данные пользователя.
		/// </summary>
		/// <value>
		/// Данные пользователя.
		/// </value>
		public Dictionary<string, string> UserData
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает данные петухов.
		/// </summary>
		/// <value>
		/// Данные петухов.
		/// </value>
		public Dictionary<string, List<RoosterDto>> RoostersData
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает данные об авторизированных пользователях.
		/// </summary>
		/// <value>
		/// Авторизированные пользователи.
		/// </value>
		public Dictionary<string, string> LoggedUsers
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает данные об игровых сессиях.
		/// </summary>
		/// <value>
		/// Игровые сессии.
		/// </value>
		public Dictionary<string, Session> Sessions
		{
			get;
			set;
		}
		#endregion
	}
}
