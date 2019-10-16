using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects
{
	/// <summary>
	/// Структура, предназначенная для сериализации петухов пользователя в XML-документ.
	/// </summary>
	[DataContract]
	public struct UserRoosters
	{
		#region Properties
		/// <summary>
		/// Возвращает или задает идентификатор клиента.
		/// </summary>
		/// <value>
		/// Идентификатор клиента.
		/// </value>
		[DataMember]
		public string ID
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает петухов клиента.
		/// </summary>
		/// <value>
		/// Петухи.
		/// </value>
		[DataMember]
		public RoosterDto[] roosters
		{
			get;
			set;
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="UserRoosters"/> структуры.
		/// </summary>
		/// <param name="roosterDictionary">The rooster dictionary.</param>
		public UserRoosters(KeyValuePair<string, List<RoosterDto>> roosterDictionary)
		{
			ID = roosterDictionary.Key;
			roosters = roosterDictionary.Value.ToArray();
		}
		#endregion
	}
}
