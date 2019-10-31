using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Класс-контракт данных, инкапсулирующий в себе характеристики петуха для редактирования.
	/// </summary>
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
		#endregion

		#region Methods
		#region Public

		public RoosterDto ToRoosterDto()
		{
			return new RoosterDto
			{
				Weight = Weight,
				Height = Height,
				Brickness = Brickness,
				Thickness = Thickness,
				Luck = Luck,
				Crest = Crest,
				ColorDto = ColorDto,
				Name = Name
			};
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
		public bool Equals(RoosterEditDto obj)
		{
			return Brickness == obj.Brickness &&
				   ColorDto == obj.ColorDto &&
				   Crest == obj.Crest &&
				   Height == obj.Height &&
				   Luck == obj.Luck &&
				   Name == obj.Name &&
				   Thickness == obj.Thickness &&
				   Weight == obj.Weight;
		}
		#endregion
		#endregion
		#endregion
	}
}
