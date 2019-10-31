using System.Threading.Tasks;
using Northis.BattleRoostersOnline.Dto;
using NUnit.Framework;

namespace Northis.BattleRoostersOnline.Service.Tests
{
	/// <summary>
	/// Тестирует сервис авторизации.
	/// </summary>
	[TestFixture]
	public class AuthenticateServiceTests : ServiceModuleTests
	{
		#region Test Methods
		#region Public

		/// <summary>
		/// Проверят возвращаемый токен.
		/// </summary>
		[TestCase("NewUser", "password")]
		public void LogIn(string login, string password)
		{
			Assert.IsNotNull(AuthenticateService.LogInAsync(login, password));
		}
		/// <summary>
		/// Проверяет реакцию сервиса на неверный логин и пароль.
		/// </summary>
		[TestCase("NewUser", "password")]
		public async Task WrongLogInData(string login, string password)
		{
			string result =  await AuthenticateService.LogInAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.WrongLoginOrPassword.ToString(), result);
		}
		/// <summary>
		/// Проверяет реакцию сервиса на вход уже авторизированного пользователя.
		/// </summary>
		[TestCase("NewUser","password")]
		public async Task AlreadyLoggedIn(string login, string password)
		{
			await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);
			string result = await AuthenticateService.LogInAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.AlreadyLoggedIn.ToString(), result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[TestCase("NewUser", "password")]
		public async Task CorrectAuthorize(string login, string password)
		{
			string result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[TestCase("NewUser", "password")]
		public async Task Register(string login, string password)
		{
			string result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет поведение сервиса при регистрации уже зарегистрированного пользователя.
		/// </summary>
		[TestCase("NewUser", "password")]
		public async Task AlreadyRegistered(string login, string password)
		{
			string result;
			await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);
			result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(result, AuthenticateStatus.AlreadyRegistered.ToString());
		}
		/// <summary>
		/// Проверяет поведение сервиса при регистрации с недопустимыми комбинациями логина и пароля.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		[TestCase("soso", "password")]
		[TestCase(null, "password")]
		[TestCase("sosssso", "pas")]
		[TestCase("sssaaoso", null)]
		[TestCase("so ssss so", "password")]
		public async Task WrongDataFormat(string login, string password)
		{
			string result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(result, AuthenticateStatus.WrongDataFormat.ToString());
		}
		/// <summary>
		/// Проверяет корректность выхода пользователя из учетной записи.
		/// </summary>
		[TestCase("NewUser", "password")]
		public async Task LogOut(string login, string password)
		{
			string token = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			bool logOutStatus = await AuthenticateService.LogOutAsync(token);

			Assert.AreEqual(logOutStatus, true);
		}
		/// <summary>
		/// Проверяет попытку выхода неавторизированного пользователя.
		/// </summary>
		[TestCase("SomeKey")]
		public async Task LogOutNotAuthorized(string token)
		{
			bool logOutStatus = await AuthenticateService.LogOutAsync(token);

			Assert.AreEqual(logOutStatus, false);
		}
		#endregion
		#endregion
	}
}
