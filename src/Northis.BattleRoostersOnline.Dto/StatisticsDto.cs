namespace Northis.BattleRoostersOnline.Dto
{
    /// <summary>
    /// Класс-контракт данных. Инкапсулирует в себе статистику.
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
			set;
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
			set;
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
			set;
		}
        #endregion
    }
}
