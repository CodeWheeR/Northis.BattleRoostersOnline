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
		[OneTimeSetUp]
		public void SetUp()
		{
			Setup();
		}

        #region Test Methods

		/// <summary>
        /// Проверят возвращаемый токен.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser", "password")]
		public void LogIn(string login, string password)
		{
			Assert.IsNotNull(AuthenticateService.LogInAsync(login, password));
		}
        /// <summary>
        /// Асинхронно проверяет реакцию сервиса на неверный логин и пароль.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль</param>
        [TestCase("NewUser1", "password")]
		public async Task WrongLogInData(string login, string password)
		{
			string result = await AuthenticateService.LogInAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.WrongLoginOrPassword.ToString(), result);
		}
        /// <summary>
        /// Асинхронно проверяет реакцию сервиса на вход уже авторизированного пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser2","password")]
		public async Task AlreadyLoggedIn(string login, string password)
		{
			await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);
			string result = await AuthenticateService.LogInAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.AlreadyLoggedIn.ToString(), result);
		}
        /// <summary>
        /// Асинхронно проверяет регистрацию нового пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser3", "password")]
		public async Task CorrectAuthorize(string login, string password)
		{
			string result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.IsNotNull(result);
		}
        /// <summary>
        /// Асинхронно проверяет регистрацию нового пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser4", "password")]
		public async Task Register(string login, string password)
		{
			string result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.IsNotNull(result);
		}
        /// <summary>
        /// Асинхронно проверяет поведение сервиса при регистрации уже зарегистрированного пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser5", "password")]
		public async Task AlreadyRegistered(string login, string password)
		{
			string result;
			await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);
			result = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.AlreadyRegistered.ToString(), result);
		}
        /// <summary>
        /// Асинхронно проверяет поведение сервиса при регистрации с недопустимыми комбинациями логина и пароля.
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

			Assert.AreEqual(AuthenticateStatus.WrongDataFormat.ToString(), result);
		}
        /// <summary>
        /// Асинхронно проверяет корректность выхода пользователя из учетной записи.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        [TestCase("NewUser6", "password")]
		public async Task LogOut(string login, string password)
		{
			string token = await AuthenticateService.RegisterAsync(login, password, CallbackAuth.Object);

			bool logOutStatus = await AuthenticateService.LogOutAsync(token);

			Assert.AreEqual(logOutStatus, true);
		}
        /// <summary>
        /// Асинхронно проверяет попытку выхода неавторизированного пользователя.
        /// </summary>
        /// <param name="token">Токен.</param>
        [TestCase("SomeKey")]
		public async Task LogOutNotAuthorized(string token)
		{
			bool logOutStatus = await AuthenticateService.LogOutAsync(token);

			Assert.AreEqual(logOutStatus, false);
		}
		#endregion
	}
}
