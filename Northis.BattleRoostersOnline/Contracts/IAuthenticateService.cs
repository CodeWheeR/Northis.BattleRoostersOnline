using System.ServiceModel;
using System.Threading.Tasks;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Contracts
{
	/// <summary>
	/// Контракт сервиса, обеспечивающего авторизацию пользователя.
	/// </summary>
	[ServiceContract(SessionMode = SessionMode.Required)]
	public interface IAuthenticateService
	{
		/// <summary>
		/// Контракт операции, отвечающей за вход пользователя в систему.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>Токен.</returns>
		[OperationContract(IsInitiating = true)]
		Task<string> LogIn(string login, string password);

		/// <summary>
		/// Контракт операции, регистрирующий нового пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns>Токен.</returns>
		[OperationContract(IsInitiating = true)]
		Task<string> Register(string login, string password);

		/// <summary>
		/// Контракт операции, отвечающей за выход пользователя из системы.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns>true - в случае успешного выхода, иначе - false.</returns>
		[OperationContract(IsTerminating = true)]
		Task<bool> LogOut(string token);

		/// <summary>
		/// Контракт операции, отвечающий за возврат статуса авторизации пользователя.
		/// </summary>
		/// <returns>AuthenticateStatus.</returns>
		[OperationContract]
		AuthenticateStatus GetLoginStatus();
	}
}
