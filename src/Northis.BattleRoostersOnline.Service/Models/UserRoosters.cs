using System.Collections.Generic;
using System.Runtime.Serialization;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Models
{
	/// <summary>
	/// Класс-контракт данных пользовательских петухов. Предназначен для сериализации петухов пользователя в XML-документ.
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
		public IEnumerable<RoosterModel> Roosters
		{
			get;
			set;
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="UserRoosters" /> структуры.
		/// </summary>
		/// <param name="roosterDictionary">The rooster dictionary.</param>
		public UserRoosters(string login, IEnumerable<RoosterModel> roosters)
		{
			Login = login;
			Roosters = roosters;
		}
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserRoosters"/> класса.
        /// </summary>
        public UserRoosters()
		{
		}
		#endregion
	}
}
