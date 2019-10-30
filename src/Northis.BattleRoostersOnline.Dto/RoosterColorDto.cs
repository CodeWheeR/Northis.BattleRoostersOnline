using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных, передающее цвет петуха.
	/// </summary>
	[DataContract]
	public enum RoosterColorDto
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
