using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using NLog;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Singleton-класс, отвечающий за обновление и отправку клиентам глобальной статистики.
	/// </summary>
	/// <seealso cref="BaseServiceWithStorage" />
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
		private List<UsersStatisticsDto> _cachedUsersStatistics = new List<UsersStatisticsDto>();

		private Logger _logger = LogManager.GetCurrentClassLogger();
		#endregion

		#region Public Methods		
		/// <summary>
		/// Запускает процесс обновления статистики.
		/// </summary>
		public async Task UpdateStatistics()
		{
			try
			{
				await Task.Run(() =>
				{
					var stats = new List<StatisticsDto>();
					var userStats = new List<UsersStatisticsDto>();
					List<(string, List<RoosterModel>)> roosters;
					lock (StorageService.RoostersData)
					{
						roosters = StorageService.RoostersData.Select(x => (x.Key, x.Value.Values.ToList())).ToList();
					}
					var loggedUsers = new List<string>();
					lock (StorageService.LoggedUsers)
					{
						loggedUsers = StorageService.LoggedUsers.Values.ToList();
					}

					foreach (var usersRoosters in roosters)
					{
						var scoresSum = usersRoosters.Item2.Sum((x) => x.WinStreak);
						userStats.Add(new UsersStatisticsDto()
						{
							UserName = usersRoosters.Item1,
							UserScore = scoresSum,
							IsOnline = loggedUsers.Contains(usersRoosters.Item1)
						});
						
					}

					foreach (var usersRoosters in roosters)
					{
						foreach (var rooster in usersRoosters.Item2)
						{
							stats.Add(new StatisticsDto()
							{
								UserName = usersRoosters.Item1,
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

					lock (_cachedUsersStatistics)
					{
						_cachedUsersStatistics = userStats.OrderByDescending(x => x.UserScore).ToList();
					}

					OnStatisticsChanged();
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw; 
			}
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
			try
			{
				if (callback is ICommunicationObject co)
					co.Closing += (x, y) => ClearSubscription(token);

				_subscribers.Add(token, callback);
				Task.Run(() =>
				{
					callback.GetNewGlobalStatistics(_cachedStatistics, _cachedUsersStatistics);
				});
			}
			catch (Exception e)
			{
				_logger.Error(e);
				throw;
			}
			
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
				_logger.Error(e);
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
						_subscribers[receiverToken].GetNewGlobalStatistics(_cachedStatistics, _cachedUsersStatistics);
					}
				}
				catch (Exception e)
				{
					if (e is CommunicationException || e is ObjectDisposedException || e is TimeoutException)
					{
						_logger.Error(e);
						if (_subscribers.ContainsKey(receiverToken) && _subscribers[receiverToken] is ICommunicationObject co)
						{
							if (co.State == CommunicationState.Opened)
							{
								co.Close();
								_subscribers.Remove(receiverToken);
							}
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
		private void ClearSubscription(string token)
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
