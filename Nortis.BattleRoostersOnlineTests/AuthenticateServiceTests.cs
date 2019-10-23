using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;

namespace Nortis.BattleRoostersOnlineTests
{
	/// <summary>
	/// Тестирует сервис авторизации.
	/// </summary>
	[TestFixture]
	public class AuthenticateServiceTests
	{
		/// <summary>
		/// Сервис авторизации.
		/// </summary>
		private AuthenticateService _authenticateService = new AuthenticateService();
		/// <summary>
		/// Настраивает тестовое окружение.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			UnityContainer container = new UnityContainer();

			container.RegisterInstance(new ServicesStorage());

			UnityServiceLocator locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}
		/// <summary>
		/// Проверят возвращаемый токен.
		/// </summary>
		[Test]
		public void LogInTest1()
		{
			Assert.IsNotNull(_authenticateService.LogInAsync("Login", "Password"));
		}
		/// <summary>
		/// Проверяет реакцию сервиса на неверный логин и пароль.
		/// </summary>
		[Test]
		public async Task LogInTest2()
		{
			string result =  await _authenticateService.LogInAsync("Login", "Password");

			Assert.AreEqual(AuthenticateStatus.WrongLoginOrPassword.ToString(), result);
		}
		/// <summary>
		/// Проверяет реакцию сервиса на вход уже авторизированного пользователя.
		/// </summary>
		[Test]
		public async Task LogInTest3()
		{
			await _authenticateService.RegisterAsync("NewUser", "password");
			string result = await _authenticateService.LogInAsync("NewUser", "password");

			Assert.AreEqual(AuthenticateStatus.AlreadyLoggedIn.ToString(), result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[Test]
		public async Task LogInTest4()
		{
			string result = await _authenticateService.RegisterAsync("NewUser", "password");

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет регистрацию нового пользователя.
		/// </summary>
		[Test]
		public async Task RegisterTest1()
		{
			string result = await _authenticateService.RegisterAsync("NewUser", "password");

			Assert.IsNotNull(result);
		}
		/// <summary>
		/// Проверяет поведение сервиса при регистрации уже зарегистрированного пользователя.
		/// </summary>
		[Test]
		public async Task RegisterTest2()
		{
			string result;
			await _authenticateService.RegisterAsync("NewUser", "password");
			result = await _authenticateService.RegisterAsync("NewUser", "password");

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
			string result = await _authenticateService.RegisterAsync(login, password);

			Assert.AreEqual(result, AuthenticateStatus.WrongDataFormat.ToString());
		}
		/// <summary>
		/// Проверяет корректность выхода пользователя из учетной записи.
		/// </summary>
		[Test]
		public async Task LogOutTest1()
		{
			ServiceLocator.Current.GetInstance<ServicesStorage>().LoggedUsers.Add("SomeKey", "SomeValue");

			bool logOutStatus = await _authenticateService.LogOutAsync("SomeKey");

			Assert.AreEqual(logOutStatus, true);
		}
		/// <summary>
		/// Проверяет попытку выхода неавторизированного пользователя.
		/// </summary>
		[Test]
		public async Task LogOutTest2()
		{
			bool logOutStatus = await _authenticateService.LogOutAsync("SomeKey");

			Assert.AreEqual(logOutStatus, false);
		}
		/// <summary>
		/// Проверяет корректность шифрования строки.
		/// </summary>
		[Test]
		public async Task EncryptTest1()
		{
			Assert.IsNotNull(_authenticateService.EncryptAsync("Text"));
		}
		/// <summary>
		/// Проверка корректность шифрования строки.
		/// </summary>
		[Test]
		public async Task EncryptTest2()
		{
			Assert.IsNotEmpty(await _authenticateService.EncryptAsync("Text"));
		}
		/// <summary>
		/// Проверяет корректность расшифровки.
		/// </summary>
		[Test]
		public async Task DecryptTest1()
		{
			string sourceText = "Hello";
			string encryptText;


			encryptText = await _authenticateService.EncryptAsync(sourceText);
			Assert.AreEqual(sourceText, _authenticateService.Decrypt(encryptText));
		}

	}
}
