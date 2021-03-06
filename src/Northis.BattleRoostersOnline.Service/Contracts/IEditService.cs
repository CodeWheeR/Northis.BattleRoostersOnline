﻿using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Contracts
{
	/// <summary>
	/// Отвечает за редактирование петухов.
	/// </summary>
	[ServiceContract]
	public interface IEditService
	{
		#region Operation Contracts
		/// <summary>
		/// Отвечает за добавление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task<bool> AddAsync(string token, RoosterCreateDto rooster);

		/// <summary>
		/// Отвечает за удаление петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="deleteRoosterToken">Удаляемый петух.</param>
		/// <returns>Task.</returns>
		[OperationContract]
		Task<bool> RemoveAsync(string token, string deleteRoosterToken);

		/// <summary>
		/// Отвечает за возврат петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Коллекцию петухов.</returns>
		[OperationContract]
		Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token);
		#endregion
	}
}
