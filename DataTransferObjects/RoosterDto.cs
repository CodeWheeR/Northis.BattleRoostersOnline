using System;
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

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Methods
		#region Public
		#region Overrided
		/// <summary>
		///  Определяет, равен ли заданный объект текущему объекту.
		/// </summary>
		/// <param name="obj">
		///  Объект, который требуется сравнить с текущим объектом.
		/// </param>
		/// <returns>
		///  Значение <see langword="true" />, если указанный объект равен текущему объекту; в противном случае — значение <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is RoosterDto)
			{
				return Equals(obj as RoosterDto);
			}
			return base.Equals(obj);
		}

		/// <summary>
		/// Определяет, равен ли заданный объект текущему объекту.
		/// </summary>
		/// <param name="obj">Объект для сравнения.</param>
		/// <returns>true - если равен; иначе - false.</returns>
		public bool Equals(RoosterDto obj)
		{
			return Brickness == obj.Brickness &&
				   ColorDto == obj.ColorDto &&
				   Crest == obj.Crest &&
				   Damage == obj.Damage &&
				   Health == obj.Health &&
				   Height == obj.Height &&
				   Hit == obj.Hit &&
				   Luck == obj.Luck &&
				   MaxHealth == obj.MaxHealth &&
				   Name == obj.Name &&
				   Stamina == obj.Stamina &&
				   Thickness == obj.Thickness &&
				   Weight == obj.Weight;
		}
		#endregion
		#endregion
		#endregion
	}

}
