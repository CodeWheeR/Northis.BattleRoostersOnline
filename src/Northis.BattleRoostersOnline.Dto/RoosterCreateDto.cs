using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет редактируемые характеристики петуха.
	/// </summary>
	[DataContract]
	public class RoosterCreateDto
	{
		#region Properties		

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
		/// Инициализует новый объект класса <see cref="RoosterCreateDto"/>.
		/// </summary>
		/// <param name="name">Имя.</param>
		/// <param name="color">Цвет.</param>
		public RoosterCreateDto(string name, RoosterColorType color)
		{
			Name = name;
			Color = color;
		}

		public RoosterCreateDto()
		{

		}
		#endregion

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
			return Color == obj.Color &&
				   Name == obj.Name;
		}
		#endregion
		#endregion
	}
}
