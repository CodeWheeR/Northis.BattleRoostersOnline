using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
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
		/// <summary>
		/// Объект, блокирующий доступ к файлу с петухами нескольким процессам.
		/// </summary>
		private object _lockerIO = new object();
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
		public async Task AddAsync(string token, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);

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
			}).ConfigureAwait(false);

			StorageService.SaveRoostersAsync();
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();
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

			return await Task.Run<IEnumerable<RoosterDto>>(() =>
			{
				if (StorageService.RoostersData.ContainsKey(login))
				{
					return StorageService.RoostersData[login];
				}

				return new List<RoosterDto>();
			}).ConfigureAwait(false);
		}

		/// <summary>
		/// Асинхронно редактирует петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="editRooster">Редактируемый петух.</param>
		public async Task EditAsync(string token, RoosterDto editRooster)
		{
			var login = await GetLoginAsync(token);
			if (StorageService.RoostersData.ContainsKey(login) &&
				StorageService.RoostersData[login].Contains(editRooster))
			{
				lock (StorageService.RoostersData)
				{
					StorageService.RoostersData[login]
								  .RemoveAll(dto => dto.Name == editRooster.Name);
					StorageService.RoostersData[login].Add(editRooster);
				}
			}
			StorageService.SaveRoostersAsync();
		}
		/// <summary>
		/// Асинхронно удаляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="deleteRooster">Удаляемый петух.</param>
		public async Task RemoveAsync(string token, RoosterDto deleteRooster)
		{
			var login = await GetLoginAsync(token);
			if (StorageService.RoostersData.ContainsKey(login) &&
				StorageService.RoostersData[login]
					   .Contains(deleteRooster))
			{
				lock (StorageService.RoostersData)
				{
					StorageService.RoostersData[login]
								  .RemoveAll(dto => dto.Name == deleteRooster.Name);
				}
			}
			StorageService.SaveRoostersAsync();
			StatisticsPublisher.GetInstance()
							   .UpdateStatistics();
		}
		#endregion
		#endregion
	}
}
