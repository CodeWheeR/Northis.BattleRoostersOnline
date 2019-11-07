using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Предоставляет сервис редактирования.
	/// </summary>
	/// <seealso cref="BaseServiceWithStorage" />
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IEditService" />
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class EditService : BaseServiceWithStorage, IEditService
	{
		#region Fields
		private Logger _logger = LogManager.GetCurrentClassLogger();
		#endregion

		#region ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="EditService"/> класса.
		/// </summary>
		/// <param name="storage">Объект хранилища. </param>
		public EditService(IDataStorageService storage) : base(storage)
		{

		}
		#endregion

        #region Public Methods
        /// <summary>
        /// Асинхронно добавляет петуха.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="rooster">Петух.</param>
        /// <returns>true, в случае успешного добавления, иначе - false.</returns>
        public async Task<bool> AddAsync(string token, RoosterEditDto rooster)
		{
			if (string.IsNullOrWhiteSpace(token) || rooster == null)
			{
				return false;
			}

			var login = await GetLoginAsync(token);
			if (login == "")
			{
				return false;
			}

			bool addingResult = true;

			try
			{
				await Task.Run(() =>
				{
					if (!StorageService.RoostersData.ContainsKey(login))
					{
						lock (StorageService.RoostersData)
						{
							StorageService.RoostersData.Add(login, new Dictionary<string, RoosterModel>());
						}
					}
					else if (StorageService.RoostersData[login].Count > 2)
					{
						addingResult = false;
						return;
					}
					lock (StorageService.RoostersData)
					{
						var battleRooster =  new RoosterModel(rooster);
						battleRooster.Token = GenerateToken(StorageService.RoostersData.ContainsKey);
						StorageService.RoostersData[login]
									  .Add(battleRooster.Token, battleRooster);
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

			return addingResult;
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
						var val = StorageService.RoostersData[login].Values.Select(x =>StorageService.Mapper.Map<RoosterModel, RoosterDto>(x));
						return val;
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
		public async Task<bool> EditAsync(string token, string sourceRoosterToken, RoosterEditDto editRooster)
		{
			if (string.IsNullOrWhiteSpace(token) ||
				string.IsNullOrWhiteSpace(sourceRoosterToken) ||
				editRooster == null)
			{
				return false;
			}

			try
			{
				await Task.Run(async () =>
				{
					var login = await GetLoginAsync(token);
					if (login == "")
					{
						return;
					}

					if (StorageService.RoostersData.ContainsKey(login) &&
						StorageService.RoostersData[login]
									  .ContainsKey(sourceRoosterToken))
					{
						var battleRooster = new RoosterModel(editRooster);
						battleRooster.Token = sourceRoosterToken;
						lock (StorageService.RoostersData)
						{
							battleRooster.WinStreak = StorageService.RoostersData[login][sourceRoosterToken]
																	.WinStreak;
							StorageService.RoostersData[login]
										  .Remove(sourceRoosterToken);

							StorageService.RoostersData[login]
										  .Add(sourceRoosterToken, battleRooster);
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
        /// <summary>
        /// Асинхронно удаляет петуха.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="deleteRoosterToken">Удаляемый петух.</param>
        /// <returns>true, в случае успешного удаления, иначе - false.</returns>
        public async Task<bool> RemoveAsync(string token, string deleteRoosterToken)
		{
			if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(deleteRoosterToken))
			{
				return false;
			}

			try
			{
				await Task.Run(async () =>
				{
					var login = await GetLoginAsync(token);
					if (StorageService.RoostersData.ContainsKey(login) &&
						StorageService.RoostersData[login]
									  .ContainsKey(deleteRoosterToken))
					{
						lock (StorageService.RoostersData)
						{
							
							StorageService.RoostersData[login]
										  .Remove(deleteRoosterToken);
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
	}
}
