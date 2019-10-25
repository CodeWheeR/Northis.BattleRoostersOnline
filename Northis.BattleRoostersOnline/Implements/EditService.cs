﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using NLog;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Models;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Класс, предоставляющий сервис редактирования.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Implements.BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IEditService" />
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class EditService : BaseServiceWithStorage, IEditService
	{
		#region Fields
		#region Private
		private Logger _logger = LogManager.GetCurrentClassLogger();
		#endregion
		#endregion

		#region Methods
		#region Public
		/// <summary>
		/// Асинхронно добавляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>Task.</returns>
		public async Task<bool> AddAsync(string token, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);

			try
			{
				await Task.Run(() =>
				{
					if (StorageService.RoostersData.ContainsKey(login))
					{
						lock (StorageService.RoostersData)
						{
							StorageService.RoostersData[login]
										  .Add(rooster);
						}
					}
					else
					{
						lock (StorageService.RoostersData)
						{
							StorageService.RoostersData.Add(login,
															new List<RoosterDto>
															{
																rooster
															});
						}
					}
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				return false;
			}

			StorageService.SaveRoostersAsync();
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();

			return true;
		}
		/// <summary>
		/// Асинхронно получает петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// Коллекцию петухов.
		/// </returns>
		public async Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token)
		{
			var login = await GetLoginAsync(token);

			try
			{
				return await Task.Run<IEnumerable<RoosterDto>>(() =>
				{
					if (StorageService.RoostersData.ContainsKey(login))
					{
						return StorageService.RoostersData[login];
					}

					return new List<RoosterDto>();
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				return new List<RoosterDto>();
			}
		}

		/// <summary>
		/// Асинхронно редактирует петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="editRooster">Редактируемый петух.</param>
		public async Task<bool> EditAsync(string token, RoosterDto sourceRooster, RoosterDto editRooster)
		{
			try
			{
				await Task.Run(async () =>
				{
					var login = await GetLoginAsync(token);
					if (StorageService.RoostersData.ContainsKey(login) &&
						StorageService.RoostersData[login]
									  .Contains(sourceRooster))
					{
						lock (StorageService.RoostersData)
						{
							StorageService.RoostersData[login]
										  .Remove(sourceRooster);
							StorageService.RoostersData[login]
										  .Add(editRooster);
						}
					}

					StorageService.SaveRoostersAsync();
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				return false;
			}

			return true;

		}
		/// <summary>
		/// Асинхронно удаляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="deleteRooster">Удаляемый петух.</param>
		public async Task<bool> RemoveAsync(string token, RoosterDto deleteRooster)
		{
			try
			{
				await Task.Run(async () =>
				{
					var login = await GetLoginAsync(token);
					if (StorageService.RoostersData.ContainsKey(login) &&
						StorageService.RoostersData[login]
									  .Contains(deleteRooster))
					{
						lock (StorageService.RoostersData)
						{
							var rooster = StorageService.RoostersData[login]
														.First(dto => dto.Name == deleteRooster.Name);
							StorageService.RoostersData[login]
										  .Remove(rooster);
						}
					}

					StorageService.SaveRoostersAsync();
					StatisticsPublisher.GetInstance()
									   .UpdateStatistics();
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				return false;
			}

			return true;
		}
		#endregion
		#endregion
	}
}
