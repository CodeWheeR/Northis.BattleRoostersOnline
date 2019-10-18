using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Extensions
{
    public static class RoosterExtensions
	{
		public static RoosterModel ToRoosterModel(this RoosterDto rooster)
		{
			return new RoosterModel()
				{
					Height = rooster.Height,
					ColorDto = rooster.ColorDto,
					Health = rooster.Health,
					Stamina = rooster.Stamina,
					Brickness = rooster.Brickness,
					Crest = rooster.Crest,
					Weight = rooster.Weight,
					WinStreak = rooster.WinStreak,
					Luck = rooster.Luck,
					Name = rooster.Name,
					Thickness = Thickness,
					MaxHealth = MaxHealth,
					Damage = Damage,
					Hit = Hit
				};
		}
    }
}
