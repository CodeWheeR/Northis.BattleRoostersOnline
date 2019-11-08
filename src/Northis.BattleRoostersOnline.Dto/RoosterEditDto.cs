using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет редактируемые характеристики петуха.
	/// </summary>
	[DataContract]
	public class RoosterEditDto
	{
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
		/// Возвращает или задает цвет петуха.
		/// </summary>
		/// <value>
		/// Цвет петуха.
		/// </value>
		[DataMember]
		public RoosterColorType Color
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или задает юркость петуха.
		/// </summary>
		/// <value>
		/// Юркость петуха.
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
		public CrestSizeType Crest
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
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализует новый объект класса <see cref="RoosterEditDto" />.
		/// </summary>
		/// <param name="name">Имя.</param>
		/// <param name="weight">Вес.</param>
		/// <param name="height">Рост.</param>
		/// <param name="brickness">Юркость.</param>
		/// <param name="thickness">Толщина покрова.</param>
		/// <param name="luck">Удача.</param>
		/// <param name="crest">Гребень.</param>
		/// <param name="color">Цвет.</param>
		public RoosterEditDto(string name, double weight, int height, int brickness, int thickness, int luck, CrestSizeType crest, RoosterColorType color)
		{
			Name = name;
			Weight = weight;
			Height = height;
			Brickness = brickness;
			Thickness = thickness;
			Luck = luck;
			Crest = crest;
			Color = color;
		}

		public RoosterEditDto()
		{
		}
		#endregion

		#region Public Methods
		#region Overrided
		/// <summary>
		/// Определяет, равен ли заданный объект текущему объекту.
		/// </summary>
		/// <param name="obj">
		/// Объект, который требуется сравнить с текущим объектом.
		/// </param>
		/// <returns>
		/// Значение <see langword="true" />, если указанный объект равен текущему объекту; в противном случае — значение
		/// <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is RoosterEditDto)
			{
				return Equals(obj as RoosterEditDto);
			}

			return base.Equals(obj);
		}

		/// <summary>
		/// Определяет, равен ли заданный объект текущему объекту.
		/// </summary>
		/// <param name="obj">Объект для сравнения.</param>
		/// <returns>true - если равен; иначе - false.</returns>
		public bool Equals(RoosterEditDto obj) =>
			Brickness == obj.Brickness &&
			Color == obj.Color &&
			Crest == obj.Crest &&
			Height == obj.Height &&
			Luck == obj.Luck &&
			Name == obj.Name &&
			Thickness == obj.Thickness &&
			Weight == obj.Weight;
		#endregion
		#endregion
	}
}
