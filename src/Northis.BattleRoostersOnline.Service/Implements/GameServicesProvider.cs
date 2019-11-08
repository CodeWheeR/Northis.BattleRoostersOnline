using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;

namespace Northis.BattleRoostersOnline.Service.Implements
{
	/// <summary>
	/// Предоставляет игровой сервис.
	/// </summary>
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IAuthenticateService" />
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IEditService" />
	/// <seealso cref="Northis.BattleRoostersOnline.Service.Contracts.IBattleService" />
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class GameServicesProvider : IAuthenticateService, IEditService, IBattleService
	{
		#region Fields
		/// <summary>
		/// Сервис редактирования.
		/// </summary>
		private readonly IEditService _editService;
		/// <summary>
		/// Сервис аутентификации.
		/// </summary>
		private readonly IAuthenticateService _authenticateService;
		/// <summary>
		/// Сервис проведения боя.
		/// </summary>
		private readonly IBattleService _battleService;
		#endregion

		#region .ctor		
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="GameServicesProvider" /> класса.
		/// </summary>
		/// <param name="authenticateService">Сервис аутентификации.</param>
		/// <param name="battleService">Сервис битвы.</param>
		/// <param name="editService">Сервис редактирования.</param>
		public GameServicesProvider(IEditService editService, IAuthenticateService authenticateService, IBattleService battleService, IDataStorageService dataStorage)
		{
			_editService = editService;
			_authenticateService = authenticateService;
			_battleService = battleService;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Асинхронно добавляет петуха.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		/// <returns>true, в случае успешного добавления, иначе - false.</returns>
		public Task<bool> AddAsync(string token, RoosterCreateDto rooster) => _editService.AddAsync(token, rooster);

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
		/// <returns>true, в случае успешного удаления, иначе - false.</returns>
		public Task<bool> RemoveAsync(string token, string deleteRooster) => _editService.RemoveAsync(token, deleteRooster);

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
		/// Статус аутентификации.
		/// </returns>
		public AuthenticateStatus GetLoginStatus() => _authenticateService.GetLoginStatus();

		/// <summary>
		/// Осуществляет поиск матча.
		/// </summary>
		/// <param name="token">Токен.</param>
		/// <param name="rooster">Петух.</param>
		public void FindMatchAsync(string token, string rooster) => _battleService.FindMatchAsync(token, rooster);

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

		/// <summary>
		/// Возвращает статус боя.
		/// </summary>
		public BattleStatus GetBattleStatus() => _battleService.GetBattleStatus();
		#endregion
	}
}
