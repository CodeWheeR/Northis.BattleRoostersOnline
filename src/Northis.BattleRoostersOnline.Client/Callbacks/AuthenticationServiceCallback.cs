using System.ComponentModel.DataAnnotations;
using System.Windows;
using Catel.IoC;
using Catel.Services;
using NLog;
using Northis.BattleRoostersOnline.Client.Models;
using Northis.BattleRoostersOnline.Client.ViewModels;
using Northis.BattleRoostersOnline.Client.GameServer;
using Northis.BattleRoostersOnline.Client.Properties;

namespace Northis.BattleRoostersOnline.Client.Callbacks
{
	/// <summary>
	/// Реализует контракт службы IAuthenticateServiceCallback.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Client.GameServer.IAuthenticateServiceCallback" />
	class AuthenticationServiceCallback : IAuthenticateServiceCallback
	{
		#region Fields
		private RoosterBrowserViewModel _roosterBrowserViewModel;

		private Logger _authServiceCallbackLogger = LogManager.GetLogger("AuthServiceCallback");
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthenticationServiceCallback"/> класса.
		/// </summary>
		/// <param name="vm">Модель-представление.</param>
		public AuthenticationServiceCallback(RoosterBrowserViewModel vm)
		{
			_roosterBrowserViewModel = vm;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Получает обновленную статистику.
		/// </summary>
		/// <param name="statistics">Статистика.</param>
		/// <param name="usersStatistics">Статистика пользователя.</param>
		public void GetNewGlobalStatistics(StatisticsDto[] statistics, UsersStatisticsDto[] usersStatistics)
		{
			_authServiceCallbackLogger.Info(Resources.StrInfoStartStatisticUpdate);
			_roosterBrowserViewModel.Statistics = new StatisticsModel[statistics.Length];
			_roosterBrowserViewModel.UserStatistics = new UserStatistic[usersStatistics.Length];
			for (int i = 0; i < statistics.Length; i++)
			{
				_roosterBrowserViewModel.Statistics[i] = new StatisticsModel(statistics[i]);
			}

			for (int i = 0; i < usersStatistics.Length; i++)
			{
				_roosterBrowserViewModel.UserStatistics[i] = new UserStatistic(usersStatistics[i]);
			}
			_authServiceCallbackLogger.Info(Resources.StrInfoStatisticUpdated);
		}
		/// <summary>
		/// Выводит пользователю сообщение, посланное сервером перед завершением работы.
		/// </summary>
		/// <param name="message">Сообщение.</param>
		public async void GetServerStopMessage(string message)
		{
			await this.GetServiceLocator()
				.ResolveType<IMessageService>()
				.ShowAsync(message, "Предупреждение");
			Application.Current.Shutdown(0);
		}
		#endregion
	}
}
