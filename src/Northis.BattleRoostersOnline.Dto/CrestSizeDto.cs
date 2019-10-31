using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных, передающее размер гребня петуха.
	/// </summary>
	[DataContract]
	public enum CrestSizeDto
	{
		[EnumMember]
		Small,
		[EnumMember]
		Medium,
		[EnumMember]
		Big
	}
}
