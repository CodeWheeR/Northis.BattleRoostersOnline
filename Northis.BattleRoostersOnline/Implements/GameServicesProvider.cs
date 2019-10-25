using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;

namespace Northis.BattleRoostersOnline.Implements
{
	/// <summary>
	/// Класс предоставляющий обобщенный игровой сервис.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IAuthenticateService" />
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IEditService" />
	/// <seealso cref="Northis.BattleRoostersOnline.Contracts.IBattleService" />
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class GameServicesProvider : IAuthenticateService, IEditService, IBattleService
	{
		#region Fields
		#region Private
		/// <summary>
		/// Сервис редактирования.
		/// </summary>
		private readonly EditService _editService;
		/// <summary>
		/// Сервис аунтефикации.
		/// </summary>
		private readonly AuthenticateService _authenticateService;
		/// <summary>
		/// Сервис проведения боя.
		/// </summary>
		private readonly BattleService _battleService;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="GameServicesProvider" /> класса.
		/// </summary>
		public GameServicesProvider()
		{
			_editService = new EditService();
			_authenticateService = new AuthenticateService();
			_battleService = new BattleService();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Асинхронно добавляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		public Task<bool> AddAsync(string token, RoosterDto rooster) =>  _editService.AddAsync(token, rooster);

		/// <summary>
		/// Асинхнронно редактирует петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="sourceRooster">Исходный петух.</param>
		/// <param name="editRooster">Редактированный петух.</param>
		public Task<bool> EditAsync(string token, RoosterDto sourceRooster, RoosterDto editRooster) => _editService.EditAsync(token, sourceRooster, editRooster);

		/// <summary>
		/// Асинхронно возвращает петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// Коллекцию петухов.
		/// </returns>
		public Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token) => _editService.GetUserRoostersAsync(token);

		/// <summary>
		/// Асинхронно удаляет петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="deleteRooster">Удаляемый петух.</param>
		public Task<bool> RemoveAsync(string token, RoosterDto deleteRooster) => _editService.RemoveAsync(token, deleteRooster);

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public Task<string> LogInAsync(string login, string password) => _authenticateService.LogInAsync(login, password);

		/// <summary>
		/// Регистрирует нового пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public Task<string> RegisterAsync(string login, string password) => _authenticateService.RegisterAsync(login, password);

		/// <summary>
		/// Осуществляет выход пользователя из системы.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешного выхода, иначе - false.
		/// </returns>
		public Task<bool> LogOutAsync(string token) => _authenticateService.LogOutAsync(token);

		/// <summary>
		/// Возвращает статус авторизации пользователя.
		/// </summary>
		/// <returns>
		/// AuthenticateStatus.
		/// </returns>
		public AuthenticateStatus GetLoginStatus() => _authenticateService.GetLoginStatus();

		/// <summary>
		/// Осуществляет поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		public void FindMatchAsync(string token, RoosterDto rooster) => _battleService.FindMatchAsync(token, rooster);

		/// <summary>
		/// Осуществляет отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешной отмены поиска, иначе - false.
		/// </returns>
		public Task<bool> CancelFinding(string token) => _battleService.CancelFinding(token);

		/// <summary>
		/// Осушествляет запуск поединка петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public void StartBattleAsync(string token, string matchToken) => _battleService.StartBattleAsync(token, matchToken);

		/// <summary>
		/// Осуществляет сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public void GiveUpAsync(string token, string matchToken) => _battleService.GiveUpAsync(token, matchToken);
		#endregion
	}
}
