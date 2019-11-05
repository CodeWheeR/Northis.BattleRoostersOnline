using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Northis.BattleRoostersOnline.Dto
{
    /// <summary>
    /// Представляет статистику пользователя.
    /// </summary>
    [DataContract]
	public class UsersStatisticsDto
	{
        #region Properties
        /// <summary>
        /// Возвращает или устанавливает значение "Находится ли пользователь в онлайне?"..
        /// </summary>
        /// <value>
        ///   <c>true</c> Если пользователь онлайн; иначе, <c>false</c>.
        /// </value>
        [DataMember]
		public bool IsOnline
		{
			get;
		}
        /// <summary>
        /// Возвращает или устанавливает значение имени пользователя.
        /// </summary>
        /// <value>
        /// Имя пользователя.
        /// </value>
        [DataMember]
		public string UserName
		{
			get;
		}
        /// <summary>
        /// Возвращает или устанавливает значение счёта пользователя.
        /// </summary>
        /// <value>
        /// Счёт пользователя.
        /// </value>
        [DataMember]
		public int UserScore
		{
			get;
		}
        #endregion

		public UsersStatisticsDto(bool isOnline, string userName, int userScore)
		{
			IsOnline = isOnline;
			UserName = userName;
			UserScore = userScore;
		}
    }
}
