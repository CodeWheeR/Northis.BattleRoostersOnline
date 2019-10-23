using System.ComponentModel.DataAnnotations;
using Northis.RoosterBattle.Properties;

namespace Northis.RoosterBattle.Models
{
	/// <summary>
	/// Окрасы петуха, от которых зависят применяемые модификации.
	/// </summary>
	internal enum RoosterColor
	{
		/// <summary>
		/// Петух получает +20 к максимальной удаче.
		/// </summary>
		[Display(Name = "Black", ResourceType = typeof(Resources))]
		Black,
		/// <summary>
		/// Петух получает +20 к максимальной толщине покрова.
		/// </summary>
		[Display(Name = "Brown", ResourceType = typeof(Resources))]
		Brown,
		/// <summary>
		/// Петух получает +20 к максимальной юркости.
		/// </summary>
		[Display(Name = "Blue", ResourceType = typeof(Resources))]
		Blue,
		/// <summary>
		/// Петух получает +20 к максимальному здоровью.
		/// </summary>
		[Display(Name = "Red", ResourceType = typeof(Resources))]
		Red,
		/// <summary>
		/// Петух получает +2 к максимальному весу.
		/// </summary>
		[Display(Name = "White", ResourceType = typeof(Resources))]
		White
	}
}
