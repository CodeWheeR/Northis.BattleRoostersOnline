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

		#region Methods
		#region Public
		/// <summary>
		/// Асинхронно добавляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		public async Task AddAsync(string token, RoosterDto rooster) => await _editService.AddAsync(token, rooster);

		/// <summary>
		/// Асинхнронно редактирует петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="roosterId">Идентификатор петуха.</param>
		/// <param name="rooster">Петух.</param>
		public async Task EditAsync(string token, int roosterId, RoosterDto rooster) => await _editService.EditAsync(token, roosterId, rooster);

		/// <summary>
		/// Асинхронно возвращает петухов пользователя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// Коллекцию петухов.
		/// </returns>
		public async Task<IEnumerable<RoosterDto>> GetUserRoostersAsync(string token) => await _editService.GetUserRoostersAsync(token);

		/// <summary>
		/// Асинхронно удаляет петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="roosterId">Идентификатор петуха.</param>
		public async Task RemoveAsync(string token, int roosterId) => _editService.RemoveAsync(token, roosterId);

		/// <summary>
		/// Осуществляет вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> LogInAsync(string login, string password) => await _authenticateService.LogInAsync(login, password);

		/// <summary>
		/// Регистрирует нового пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>
		/// Токен.
		/// </returns>
		public async Task<string> RegisterAsync(string login, string password) => await _authenticateService.RegisterAsync(login, password);

		/// <summary>
		/// Осуществляет выход пользователя из системы.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешного выхода, иначе - false.
		/// </returns>
		public async Task<bool> LogOutAsync(string token) => await _authenticateService.LogOutAsync(token);

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
		public async Task FindMatchAsync(string token, RoosterDto rooster) => _battleService.FindMatchAsync(token, rooster);

		/// <summary>
		/// Осуществляет отмену поиска матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <returns>
		/// true - в случае успешной отмены поиска, иначе - false.
		/// </returns>
		public bool CancelFinding(string token) => _battleService.CancelFinding(token);

		/// <summary>
		/// Осушествляет запуск поединка петухов.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public async Task StartBattleAsync(string token, string matchToken) => _battleService.StartBattleAsync(token, matchToken);

		/// <summary>
		/// Нереализованный контракт операции.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <exception cref="NotImplementedException"></exception>
		public async Task Beak(string token, string matchToken) => throw new NotImplementedException();

		/// <summary>
		/// Нереализованный контракт операции.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <exception cref="NotImplementedException"></exception>
		public async Task Bite(string token, string matchToken) => throw new NotImplementedException();

		/// <summary>
		/// Нереализованный контракт операции.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		/// <exception cref="NotImplementedException"></exception>
		public async Task Pull(string token, string matchToken) => throw new NotImplementedException();

		/// <summary>
		/// Осуществляет сдачу боя.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="matchToken">Токен матча.</param>
		public async Task GiveUpAsync(string token, string matchToken) => await _battleService.GiveUpAsync(token, matchToken);
		#endregion
		#endregion
	}
}
