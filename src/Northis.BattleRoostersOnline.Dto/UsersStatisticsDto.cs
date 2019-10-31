using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Dto
{
	[DataContract]
	public class UsersStatisticsDto
	{
		/// <summary>
		/// Возвращает или устанавливает значение находится ли пользователь в онлайне.
		/// </summary>
		[DataMember]
		public bool IsOnline
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или устанавливает значение имени пользователя.
		/// </summary>
		[DataMember]
		public string UserName
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или устанавливает значение количества побед пользователя.
		/// </summary>
		[DataMember]
		public int UserScore
		{
			get;
			set;
		}
	}
}
