using System.Runtime.Serialization;

namespace DataTransferObjects
{
	/// <summary>
	/// Класс-контракт данных, инкапсулирующий в себе характеристики петуха.
	/// </summary>
	[DataContract]
	public class RoosterDto
    {
		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="RoosterDto"/> класса.
		/// </summary>
		public RoosterDto()
		{

		}
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает или задает вес петуха.
		/// </summary>
		/// <value>
		/// Вес петуха.
		/// </value>
		[DataMember]
		public double Weight
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает рост петуха.
		/// </summary>
		/// <value>
		/// Рост петуха.
		/// </value>
		[DataMember]
		public int Height
		{
			get;
			set;
		}
		/// <summary>
		/// Врзвращает или задает здоровье петуха.
		/// </summary>
		/// <value>
		/// Здоровье петуха.
		/// </value>
		[DataMember]
		public double Health
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает выносливость петуха.
		/// </summary>
		/// <value>
		/// Выносливость петуха.
		/// </value>
		[DataMember]
		public int Stamina
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает цвет петуха.
		/// </summary>
		/// <value>
		/// Цвет петуха.
		/// </value>
		[DataMember]
		public RoosterColorDto ColorDto
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает уклонение петуха.
		/// </summary>
		/// <value>
		/// Плотность петуха.
		/// </value>
		[DataMember]
		public int Brickness
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает размер гребня петуха.
		/// </summary>
		/// <value>
		/// Размер гребня.
		/// </value>
		[DataMember]
		public CrestSizeDto Crest
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает плотность петуха.
		/// </summary>
		/// <value>
		/// Плотность петуха.
		/// </value>
		[DataMember]
		public int Thickness
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает удачу петуха.
		/// </summary>
		/// <value>
		/// Удача петуха.
		/// </value>
		[DataMember]
		public int Luck
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает имя петуха.
		/// </summary>
		/// <value>
		/// Имя петуха.
		/// </value>
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает серию побед петуха.
		/// </summary>
		/// <value>
		/// Серия побед.
		/// </value>
		[DataMember]
		public int WinStreak
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает силу удара петуха.
		/// </summary>
		/// <value>
		/// Сила удара.
		/// </value>
		[DataMember]
		public double Hit
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает урон петуха.
		/// </summary>
		/// <value>
		/// Урон петуха.
		/// </value>
		[DataMember]
		public double Damage
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает максимальное здоровье петуха.
		/// </summary>
		/// <value>
		/// Максимальное здоровье.
		/// </value>
		[DataMember]
		public int MaxHealth
		{
			get;
			set;
		}
		#endregion
	}
}
