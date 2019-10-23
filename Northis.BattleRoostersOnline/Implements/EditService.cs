using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;

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

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="EditService"/> класса.
		/// </summary>
		public EditService()
		{
			Load();
		}
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
				if (Storage.RoostersData.ContainsKey(login))
				{
					lock (Storage.RoostersData)
					{
						Storage.RoostersData[login]
							   .Add(rooster);
					}
				}
				else
				{
					lock (Storage.RoostersData)
					{
						Storage.RoostersData.Add(login,
												 new List<RoosterDto>
												 {
													 rooster
												 });
					}
				}
			}).ConfigureAwait(false);

			Storage.SaveRoostersAsync();
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
				if (Storage.RoostersData.ContainsKey(login))
				{
					return Storage.RoostersData[login];
				}

				return new List<RoosterDto>();
			}).ConfigureAwait(false);
		}

		/// <summary>
		/// Асинхронно редактирует петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="roosterSeqNum">Порядковый номер петуха.</param>
		/// <param name="rooster">Петух.</param>
		public async Task EditAsync(string token, int roosterSeqNum, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) &&
				Storage.RoostersData[login]
					   .Count >
				roosterSeqNum &&
				roosterSeqNum >= 0)
			{
				lock (Storage.RoostersData)
				{
					Storage.RoostersData[login][roosterSeqNum] = rooster;
				}
			}

			Storage.SaveRoostersAsync();
		}
		/// <summary>
		/// Асинхронно удаляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="roosterSeqNum">Порядковый номер петуха.</param>
		public async Task RemoveAsync(string token, int roosterSeqNum)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) &&
				Storage.RoostersData[login]
					   .Count >
				roosterSeqNum &&
				roosterSeqNum >= 0)
			{
				lock (Storage.RoostersData)
				{
					Storage.RoostersData[login]
						   .RemoveAt(roosterSeqNum);
				}
			}
			Storage.SaveRoostersAsync();
		}
		#endregion
		#endregion
	}
}
