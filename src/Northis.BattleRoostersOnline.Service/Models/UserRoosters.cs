using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Service.Models
{
	/// <summary>
	/// Отвечает за хранения пользователей и петухов в XML-файле.
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
		/// Инициализирует новый объект <see cref="UserRoosters" /> класса.
		/// </summary>
		/// <param name="roosterDictionary">The rooster dictionary.</param>
		public UserRoosters(string login, IEnumerable<RoosterModel> roosters)
		{
			Login = login;
			Roosters = roosters;
		}

		/// <summary>
		/// Инициализует новый объект  <see cref="UserRoosters" /> класса для корректной работы сериализатора.
		/// </summary>
		public UserRoosters()
		{
		}
		#endregion
	}
}
