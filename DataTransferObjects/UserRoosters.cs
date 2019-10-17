using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects
{
	/// <summary>
	/// Структура, предназначенная для сериализации петухов пользователя в XML-документ.
	/// </summary>
	[DataContract]
	public class UserRoosters
	{
		#region Properties
		/// <summary>
		/// Возвращает или задает идентификатор клиента.
		/// </summary>
		/// <Roosters>
		/// Идентификатор клиента.
		/// </Roosters>
		[DataMember]
		public string Login
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает петухов клиента.
		/// </summary>
		/// <Roosters>
		/// Петухи.
		/// </Roosters>
		[DataMember]
		public IEnumerable<RoosterDto> Roosters
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
		public UserRoosters(string login, List<RoosterDto> roosters)
		{
			Login = login;
			Roosters = roosters;
		}
		#endregion
	}
}
