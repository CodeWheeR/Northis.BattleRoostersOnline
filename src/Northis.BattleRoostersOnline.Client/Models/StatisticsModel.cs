using Catel.ComponentModel;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Представляет игровую статистику.
	/// </summary>
	class StatisticsModel
	{
		#region Public Properties
		/// <summary>
		/// Возвращает или задает имя пользователя.
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
		/// Возвращает или задает имя петуха.
		/// </summary>
		/// <value>
		/// Имя петуха.
		/// </value>
		[DisplayName("Имя петуха")]
		public string RoosterName
		{
			get;
		}
		/// <summary>
		/// Возвращает или задает череду побед.
		/// </summary>
		/// <value>
		/// Череда побед.
		/// </value>
		[DisplayName("Череда побед")]
		public int WinStreak
		{
			get;
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует объект <see cref="StatisticsModel"/> класса.
		/// </summary>
		/// <param name="source">Источник.</param>
		public StatisticsModel(StatisticsDto source = null)
		{
			if (source != null)
			{
				UserName = source.UserName;
				RoosterName = source.RoosterName;
				WinStreak = source.WinStreak;
			}
		}
		#endregion
	}
}
