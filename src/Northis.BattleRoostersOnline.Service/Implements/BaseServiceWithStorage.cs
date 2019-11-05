using System;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Service.DataStorages;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Представляет доступ к хранилищу  основных данных программы.
	/// ServiceStorage.
	/// </summary>
	public abstract class BaseServiceWithStorage : BaseService
	{
		#region Properties
		/// <summary>
		/// Возвращает или устанавливает хранилище основных данных.
		/// </summary>
		/// <value>
		/// Хранилище данных.
		/// </value>
		/// <exception cref="NullReferenceException">Хранилище не инициализированно экземпляром класса.</exception>
		public static IDataStorageService StorageService
		{
			get;
			set;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Устанавливает хранилище данных.
		/// </summary>
		/// <param name="service">Хранилище данных.</param>
		public static void SetStorage(IDataStorageService service)
		{
			StorageService = service;
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Асинхронно возвращает логин пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>Логин.</returns>
		protected async Task<string> GetLoginAsync(string token) =>
			await Task.Run(() =>
			{
				if (!string.IsNullOrWhiteSpace(token) && StorageService.LoggedUsers.ContainsKey(token))
				{
					return StorageService.LoggedUsers[token];
				}

				return "";
			});

		#endregion
	}
}
