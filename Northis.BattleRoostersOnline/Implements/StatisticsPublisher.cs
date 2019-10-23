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
	class StatisticsPublisher : BaseServiceWithStorage
	{
		private static StatisticsPublisher _instance;
		private event EventHandler<GlobalStatisticsEventArgs> StatisticsChanged;
		private readonly Dictionary<string, EventHandler<GlobalStatisticsEventArgs>> _subscribers = new Dictionary<string, EventHandler<GlobalStatisticsEventArgs>>();

		private List<StatisticsDto> _cachedStatistics
		{
			get;
			set;
		} = new List<StatisticsDto>();

		public async Task UpdateStatistics()
		{
			await Task.Run(() =>
			{
				var stats = new List<StatisticsDto>();
				lock (Storage.RoostersData)
				{
					foreach (var usersRoosters in Storage.RoostersData)
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

		public List<StatisticsDto> GetGlobalStatistics() => _cachedStatistics;

		public void Subscribe(string token, IAuthenticateServiceCallback callback)
		{
			EventHandler<GlobalStatisticsEventArgs> callbackOperation = (x, y) => callback.GetNewGlobalStatistics(y.Statistics);
			_subscribers.Add(token, callbackOperation);
			StatisticsChanged += callbackOperation;
			Task.Run(() => {
				callback.GetNewGlobalStatistics(_cachedStatistics);
			});
		}

		public void Unsubscribe(string token)
		{
			StatisticsChanged -= _subscribers[token];
			_subscribers.Remove(token);
		}

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

		public static async Task<StatisticsPublisher> GetInstanceAsync()
		{
			if (_instance == null)
			{
				_instance = new StatisticsPublisher();
				await _instance.UpdateStatistics();
			}

			return _instance;
		}

		public static StatisticsPublisher GetInstance()
		{
			if (_instance == null)
			{
				_instance = new StatisticsPublisher();
#pragma warning disable 4014
				_instance.UpdateStatistics();
#pragma warning restore 4014
			}

			return _instance;
		}

		public async Task SendStatistics(string receiverToken)
		{
			await Task.Run(() =>
			{
				try
				{
					if (_subscribers.ContainsKey(receiverToken))
					{
						_subscribers[receiverToken]
							.Invoke(this,
									new GlobalStatisticsEventArgs()
									{
										Statistics = _cachedStatistics
									});
					}
				}
				catch (Exception e)
				{
					Type type = e.GetType();
					if (e is CommunicationException || e is ObjectDisposedException || e is TimeoutException)
					{
						_subscribers.Remove(receiverToken);
						Storage.LoggedUsers.Remove(receiverToken);
					}

					throw;
				}
			});
		}

	}
}
