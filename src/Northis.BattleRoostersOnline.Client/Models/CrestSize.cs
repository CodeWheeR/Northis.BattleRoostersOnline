using System.ComponentModel.DataAnnotations;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Представляет собой размер гребня, влияющий на урон петуха.
	/// </summary>
	internal enum CrestSize
	{
		/// <summary>
		/// Повышает урон на 0%.
		/// </summary>
		[Display(Name = "SmallCrest", ResourceType = typeof(Resources))]
		Small,
		/// <summary>
		/// Повышает урон на 25%.
		/// </summary>
		[Display(Name = "MediumCrest", ResourceType = typeof(Resources))]
		Medium,
		/// <summary>
		/// Повышает урон на 50%.
		/// </summary>
		[Display(Name = "HugeCrest", ResourceType = typeof(Resources))]
		Big
		
	}
}
