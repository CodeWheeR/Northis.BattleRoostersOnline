using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Перечисление-контракт данных. Передает размер гребня петуха.
	/// </summary>
	[DataContract]
	public enum CrestSizeType
	{
		/// <summary>
		/// Повышает урон на 0%.
		/// </summary>
		[EnumMember]
		Small,
		/// <summary>
		/// Повышает урон на 25%.
		/// </summary>
		[EnumMember]
		Medium,
		/// <summary>
		/// Повышает урон на 50%.
		/// </summary>
		[EnumMember]
		Big
	}
}
