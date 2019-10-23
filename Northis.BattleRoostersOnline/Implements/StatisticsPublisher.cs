using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Events;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Singleton-класс, отвечающий за обновление и отправку клиентам глобальной статистики.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Implements.BaseServiceWithStorage" />
	class StatisticsPublisher : BaseServiceWithStorage
	{
		#region Fields
		private static StatisticsPublisher _instance;
		/// <summary>
		/// Словарь колбеков клиентов для отправки статистики, ключ - токен пользователя.
		/// </summary>
		private readonly Dictionary<string, IAuthenticateServiceCallback> _subscribers = new Dictionary<string, IAuthenticateServiceCallback>();
		/// <summary>
		/// Кэшированное значение статистики
		/// </summary>
		private List<StatisticsDto> _cachedStatistics = new List<StatisticsDto>();
		#endregion

		#region Public Methods		
		/// <summary>
		/// Запускает процесс обновления статистики.
		/// </summary>
		public async Task UpdateStatistics()
		{
			await Task.Run(() =>
			{
				var stats = new List<StatisticsDto>();
				lock (StorageService.RoostersData)
				{
					foreach (var usersRoosters in StorageService.RoostersData)
					{
						var rooster = usersRoosters.Value.First(r => r.WinStreak == usersRoosters.Value.Max(m => m.WinStreak));
						stats.Add(new StatisticsDto()
						{
							UserName = usersRoosters.Key,
							RoosterName = rooster.Name,
							WinStreak = rooster.WinStreak
						});
					}
				}

				lock (_cachedStatistics)
				{
					_cachedStatistics = stats.OrderByDescending(x => x.WinStreak)
											 .ToList();
				}

				OnStatisticsChanged();
			});
		}

		/// <summary>
		/// Возвращает текущую статистику.
		/// </summary>
		/// <returns></returns>
		public List<StatisticsDto> GetGlobalStatistics() => _cachedStatistics;

		/// <summary>
		/// Выполняет подписку пользователя на обновление статистики.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="callback">The callback.</param>
		public void Subscribe(string token, IAuthenticateServiceCallback callback)
		{
			if (callback is ICommunicationObject co)
				co.Closing += (x, y) => ClearSubscribtion(token);

			_subscribers.Add(token, callback);
			Task.Run(() => {
				callback.GetNewGlobalStatistics(_cachedStatistics);
			});
		}

		/// <summary>
		/// Выполняет отписку пользователя от обновлений.
		/// </summary>
		/// <param name="token">The token.</param>
		public void Unsubscribe(string token)
		{
			_subscribers.Remove(token);
		}

		/// <summary>
		/// Асинхронно возвращает объект класса <see cref="StatisticsPublisher"/>.
		/// </summary>
		public static async Task<StatisticsPublisher> GetInstanceAsync()
		{
			if (_instance == null)
			{
				_instance = new StatisticsPublisher();
				await _instance.UpdateStatistics();
			}

			return _instance;
		}

		/// <summary>
		/// Возращает объект класса <see cref="StatisticsPublisher"/>.
		/// </summary>
		public static StatisticsPublisher GetInstance()
		{
			if (_instance == null)
			{
				_instance = new StatisticsPublisher();
			}

			return _instance;
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Выполняется при изменении статистики.
		/// </summary>
		private async void OnStatisticsChanged()
		{
			try
			{
				var subKeys = _subscribers.Keys.ToArray();
				foreach (var key in subKeys)
				{
					SendStatistics(key);
				}
			}
			catch (CommunicationException e)
			{
				Debug.WriteLine(e.TargetSite + ": " + e.Message);
			}
		}

		/// <summary>
		/// Выполняет отправку статистики пользователю с указанным токеном.
		/// </summary>
		/// <param name="receiverToken">Токен получателя.</param>
		private async Task SendStatistics(string receiverToken)
		{
			await Task.Run(() =>
			{
				try
				{
					if (_subscribers.ContainsKey(receiverToken))
					{
						_subscribers[receiverToken].GetNewGlobalStatistics(_cachedStatistics);
					}
				}
				catch (Exception e)
				{
					if (e is CommunicationException || e is ObjectDisposedException || e is TimeoutException)
					{
						if (_subscribers.ContainsKey(receiverToken) && _subscribers[receiverToken] is ICommunicationObject co)
						{
							co.Close();
						}
					}
					else
					{
						throw;
					}
				}
			});
		}
		/// <summary>
		/// Выполняет отписку пользователя от оповещений.
		/// </summary>
		/// <param name="token">Токен пользователя.</param>
		private void ClearSubscribtion(string token)
		{
			if (_subscribers.ContainsKey(token))
			{
				_subscribers.Remove(token);
			}

			if (StorageService.LoggedUsers.ContainsKey(token))
			{
				StorageService.LoggedUsers.Remove(token);
			}
		}
		#endregion
	}
}
