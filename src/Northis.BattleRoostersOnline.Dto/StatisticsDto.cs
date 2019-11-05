namespace Northis.BattleRoostersOnline.Dto
{
    /// <summary>
    /// Представляет глобальную статистику.
    /// </summary>
    public class StatisticsDto
	{
        #region Properties        
        /// <summary>
        /// Возвращает или задает имя пользователя.
        /// </summary>
        /// <value>
        /// Имя пользователя.
        /// </value>
        public string UserName
		{
			get;
		}
        /// <summary>
        /// Возвращает или задает имя петуха.
        /// </summary>
        /// <value>
        /// Имя петуха.
        /// </value>
        public string RoosterName
		{
			get;
		}
        /// <summary>
        /// Возвращает или задает количество побед.
        /// </summary>
        /// <value>
        /// Количество побед.
        /// </value>
        public int WinStreak
		{
			get;
		}
        #endregion

		public StatisticsDto(string userName, string roosterName, int winStreak)
		{
			UserName = userName;
			RoosterName = roosterName;
			WinStreak = winStreak;
		}
    }
}
