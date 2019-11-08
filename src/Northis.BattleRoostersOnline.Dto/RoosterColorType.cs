using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет возможные окрасы петуха.
	/// </summary>
	[DataContract]
	public enum RoosterColorType
	{
		/// <summary>
		/// Петух получает +20 к максимальной удаче.
		/// </summary>
		[EnumMember]
		Black,
		/// <summary>
		/// Петух получает +20 к максимальной толщине покрова.
		/// </summary>
		[EnumMember]
		Brown,
		/// <summary>
		/// Петух получает +20 к максимальной юркости.
		/// </summary>
		[EnumMember]
		Blue,
		/// <summary>
		/// Петух получает +20 к максимальному здоровью.
		/// </summary>
		[EnumMember]
		Red,
		/// <summary>
		/// Петух получает +2 к максимальному весу.
		/// </summary>
		[EnumMember]
		White
	}
}
