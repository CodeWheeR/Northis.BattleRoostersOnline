using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных. Передает цвет петуха.
	/// </summary>
	[DataContract]
	public enum RoosterColor
	{
		[EnumMember]
		Black,
		[EnumMember]
		Brown,
		[EnumMember]
		Blue,
		[EnumMember]
		Red,
		[EnumMember]
		White
	}
}
