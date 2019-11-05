using System;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет характеристики петуха.
	/// </summary>
	[DataContract]
	public class RoosterDto
	{
		#region Properties		
		/// <summary>
		/// Возвращает токен.
		/// </summary>
		/// <value>
		/// Токен.
		/// </value>
		[DataMember]
		public string Token
		{
			get;
		}

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
		}

		/// <summary>
		/// Возвращает или задает цвет петуха.
		/// </summary>
		/// <value>
		/// Цвет петуха.
		/// </value>
		[DataMember]
		public RoosterColorType ColorType
		{
			get;
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
		}

        #endregion

		/// <summary>
		/// Инициализует новый объект класса <see cref="RoosterDto"/>.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="name">Имя.</param>
		/// <param name="weight">Вес.</param>
		/// <param name="height">Рост.</param>
		/// <param name="brickness">Юркость.</param>
		/// <param name="thickness">Толщина покрова.</param>
		/// <param name="luck">Удача.</param>
		/// <param name="health">Здоровье.</param>
		/// <param name="crest">Гребень.</param>
		/// <param name="colorType">Цвет.</param>
		/// <param name="maxHealth">Максимальное здоровье.</param>
		/// <param name="stamina">Выносливость.</param>
		/// <param name="winStreak">Череда побед.</param>
		public RoosterDto(string token, string name, double weight, int height, int brickness, int thickness, int luck, double health, CrestSizeType crest, RoosterColorType colorType, int maxHealth, int stamina, int winStreak)
		{
			Token = token;
			Name = name;
			Weight = weight;
			Height = height;
			Brickness = brickness;
			Thickness = thickness;
			Luck = luck;
			Health = health;
			MaxHealth = maxHealth;
			Stamina = stamina;
			WinStreak = winStreak;
			Crest = crest;
			ColorType = colorType;
		}

        #region Public Methods

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
				   ColorType == obj.ColorType &&
				   Crest == obj.Crest &&
				   Health == obj.Health &&
				   Height == obj.Height &&
				   Luck == obj.Luck &&
				   MaxHealth == obj.MaxHealth &&
				   Name == obj.Name &&
				   Stamina == obj.Stamina &&
				   Thickness == obj.Thickness &&
				   Weight == obj.Weight;
		}
		#endregion
		#endregion
	}

}
