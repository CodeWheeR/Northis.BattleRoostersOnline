using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных. Передает размер гребня петуха.
	/// </summary>
	[DataContract]
	public enum CrestSize
	{
		[EnumMember]
		Small,
		[EnumMember]
		Medium,
		[EnumMember]
		Big
	}
}
