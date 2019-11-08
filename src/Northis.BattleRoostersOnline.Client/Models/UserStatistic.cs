using System.ComponentModel;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Представляет статистику пользователя.
	/// </summary>
	internal class UserStatistic
	{
		#region Public Properties
		/// <summary>
		/// Возвращает или задает статус пользователя.
		/// </summary>
		/// <value>
		/// <c>true</c> если пользователь онлайн; Иначе, <c>false</c>.
		/// </value>
		[DisplayName("В сети")]
		public bool IsOnline
		{
			get;
		}

		/// <summary>
		/// Возвращает имя пользователя.
		/// </summary>
		/// <value>
		/// Имя пользователя.
		/// </value>
		[DisplayName("Пользователь")]
		public string UserName
		{
			get;
		}

		/// <summary>
		/// Возвращает количество побед пользователя.
		/// </summary>
		/// <value>
		/// Количество побед.
		/// </value>
		[DisplayName("Количество побед")]
		public int UserScore
		{
			get;
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="UserStatistic" /> класса.
		/// </summary>
		/// <param name="source">Источник.</param>
		public UserStatistic(UsersStatisticsDto source)
		{
			UserName = source.UserName;
			UserScore = source.UserScore;
			IsOnline = source.IsOnline;
		}
		#endregion
	}
}
