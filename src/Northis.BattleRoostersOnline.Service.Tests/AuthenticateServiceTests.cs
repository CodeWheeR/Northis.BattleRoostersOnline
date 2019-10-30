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
		#region Methods
		#region Public

		/// <summary>
		/// Проверят возвращаемый токен.
		/// </summary>
		[Test]
		public void LogInTest1()
		{
			Assert.IsNotNull(authenticateService.LogInAsync("Login", "Password"));
		}
		/// <summary>
		/// Проверяет реакцию сервиса на неверный логин и пароль.
		/// </summary>
		[Test]
		public async Task LogInTest2()
		{
			string result =  await authenticateService.LogInAsync("Login1", "Password", callbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.WrongLoginOrPassword.ToString(), result);
		}
		/// <summary>
		/// Проверяет реакцию сервиса на вход уже авторизированного пользователя.
		/// </summary>
		[Test]
		public async Task LogInTest3()
		{
			await authenticateService.RegisterAsync("NewUser", "password", callbackAuth.Object);
			string result = await authenticateService.LogInAsync("NewUser", "password", callbackAuth.Object);

			Assert.AreEqual(AuthenticateStatus.AlreadyLoggedIn.ToString(), result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[Test]
		public async Task LogInTest4()
		{
			string result = await authenticateService.RegisterAsync("NewUser", "password", callbackAuth.Object);

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[Test]
		public async Task RegisterTest1()
		{
			string result = await authenticateService.RegisterAsync("NewUser", "password", callbackAuth.Object);

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет поведение сервиса при регистрации уже зарегистрированного пользователя.
		/// </summary>
		[Test]
		public async Task RegisterTest2()
		{
			string result;
			await authenticateService.RegisterAsync("NewUser", "password", callbackAuth.Object);
			result = await authenticateService.RegisterAsync("NewUser", "password", callbackAuth.Object);

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
		public async Task RegisterTest2(string login, string password)
		{
			string result = await authenticateService.RegisterAsync(login, password, callbackAuth.Object);

			Assert.AreEqual(result, AuthenticateStatus.WrongDataFormat.ToString());
		}
		/// <summary>
		/// Проверяет корректность выхода пользователя из учетной записи.
		/// </summary>
		[Test]
		public async Task LogOutTest1()
		{
			string token = await authenticateService.RegisterAsync("Login", "Password", callbackAuth.Object);

			bool logOutStatus = await authenticateService.LogOutAsync(token);

			Assert.AreEqual(logOutStatus, true);
		}
		/// <summary>
		/// Проверяет попытку выхода неавторизированного пользователя.
		/// </summary>
		[Test]
		public async Task LogOutTest2()
		{
			bool logOutStatus = await authenticateService.LogOutAsync("SomeKey");

			Assert.AreEqual(logOutStatus, false);
		}
		/// <summary>
		/// Проверяет корректность шифрования строки.
		/// </summary>
		[Test]
		public async Task EncryptTest1()
		{
			Assert.IsNotNull(authenticateService.EncryptAsync("Text"));
		}
		/// <summary>
		/// Проверка корректность шифрования строки.
		/// </summary>
		[Test]
		public async Task EncryptTest2()
		{
			Assert.IsNotEmpty(await authenticateService.EncryptAsync("Text"));
		}
		/// <summary>
		/// Проверяет корректность расшифровки.
		/// </summary>
		[Test]
		public async Task DecryptTest1()
		{
			string sourceText = "Hello";
			string encryptText;


			encryptText = await authenticateService.EncryptAsync(sourceText);
			Assert.AreEqual(sourceText, authenticateService.Decrypt(encryptText));
		}
		#endregion
		#endregion
	}
}
