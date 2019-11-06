using System.Runtime.Serialization;

namespace Northis.BattleRoostersOnline.Dto
{
	/// <summary>
	/// Представляет глобальную статистику.
	/// </summary>
	[DataContract]
	public class StatisticsDto
	{
        #region Properties        
        /// <summary>
        /// Возвращает или задает имя пользователя.
        /// </summary>
        /// <value>
        /// Имя пользователя.
        /// </value>
        [DataMember]
        public string UserName
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

		public string RoosterName
		{
			get;
			set;
		}
		/// <summary>
		/// Возвращает или задает количество побед.
		/// </summary>
		/// <value>
		/// Количество побед.
		/// </value>
		[DataMember]

		public int WinStreak
		{
			get;
			set;
		}
		#endregion

		/// <summary>
		/// Инициализует новый объект класса <see cref="StatisticsDto"/>.
		/// </summary>
		/// <param name="userName">Имя пользователя.</param>
		/// <param name="roosterName">Имя петуха.</param>
		/// <param name="winStreak">Череда побед.</param>
		public StatisticsDto(string userName, string roosterName, int winStreak)
		{
			UserName = userName;
			RoosterName = roosterName;
			WinStreak = winStreak;
		}

		public StatisticsDto()
		{

		}
    }
}
