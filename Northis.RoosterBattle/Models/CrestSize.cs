using System.ComponentModel.DataAnnotations;
using Northis.RoosterBattle.Properties;

namespace Northis.RoosterBattle.Models
{
	/// <summary>
	/// Размер гребня. Повышает урон
	/// </summary>
	internal enum CrestSize
	{
		/// <summary>
		/// Повышает урон на 0%
		/// </summary>
		[Display(Name = "SmallCrest", ResourceType = typeof(Resources))]
		Small,
		/// <summary>
		/// Повышает урон на 25%
		/// </summary>
		[Display(Name = "MediumCrest", ResourceType = typeof(Resources))]
		Medium,
		/// <summary>
		/// Повышает урон на 50%
		/// </summary>
		[Display(Name = "HugeCrest", ResourceType = typeof(Resources))]
		Big
	}
}
