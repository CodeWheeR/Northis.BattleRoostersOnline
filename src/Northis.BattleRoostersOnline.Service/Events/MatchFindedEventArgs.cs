﻿using System;

namespace Northis.BattleRoostersOnline.Service.Events
{
	/// <summary>
	/// Хранит информацию процесса поиска матча.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	internal class MatchFindedEventArgs : EventArgs
	{
		#region Properties
		/// <summary>
		/// Задает или предоставляет токен матча.
		/// </summary>
		/// <value>
		/// Токен матча.
		/// </value>
		public string MatchToken
		{
			get;
			set;
		}
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="MatchFindedEventArgs" /> класса.
		/// </summary>
		/// <param name="token">Токен.</param>
		public MatchFindedEventArgs(string token) => MatchToken = token;
		#endregion
	}
}
