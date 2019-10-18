using System.ComponentModel.DataAnnotations;

namespace Northis.BattleRoostersOnline.Models
{
	/// <summary>
	/// Окрасы петуха, от которых зависят применяемые модификации.
	/// </summary>
	enum RoosterColor
	{
		/// <summary>
		/// Петух получает +20 к максимальной удаче.
		/// </summary>
		Black,
		/// <summary>
		/// Петух получает +20 к максимальной толщине покрова.
		/// </summary>
		Brown,
		/// <summary>
		/// Петух получает +20 к максимальной юркости.
		/// </summary>
		Blue,
		/// <summary>
		/// Петух получает +20 к максимальному здоровью.
		/// </summary>
		Red,
		/// <summary>
		/// Петух получает +2 к максимальному весу.
		/// </summary>
		White
	}
}
