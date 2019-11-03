using System;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Класс-контракт данных. Инкапсулирует в себе характеристики петуха.
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
			set;
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
		public RoosterColor Color
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
		public CrestSize Crest
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

        #region Public Methods

        /// <summary>
        /// Не реализованный метод сравнения.
        /// </summary>
        /// <param name="obj">Объект сравнения.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

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
				   Color == obj.Color &&
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
