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
	[TestFixture]
	public class AuthenticateServiceTests
	{
		private AuthenticateService _authenticateService = new AuthenticateService();


		[SetUp]
		public void Setup()
		{
			UnityContainer container = new UnityContainer();

			container.RegisterInstance(new ServicesStorage
			{
				RoostersData = new Dictionary<string, List<RoosterDto>>(),
				UserData = new Dictionary<string, string>(),
				LoggedUsers = new Dictionary<string, string>(),
				Sessions = new Dictionary<string, Session>()
			});

			UnityServiceLocator locator = new UnityServiceLocator(container);

			ServiceLocator.SetLocatorProvider(() => locator);
		}


		[Test]
		public void LogInTest1()
		{
			Assert.IsNotNull(_authenticateService.LogIn("Login", "Password"));
		}

		[Test]
		public async Task LogInTest2()
		{
			string result =  await _authenticateService.LogIn("Login", "Password");
			Assert.AreEqual(AuthenticateStatus.WrongLoginOrPassword.ToString(), result);
		}

		[Test]
		public async Task LogInTest3()
		{
			await _authenticateService.Register("NewUser", "password");
			string result = await _authenticateService.LogIn("NewUser", "password");


			Assert.AreEqual(AuthenticateStatus.AlreadyLoggedIn.ToString(), result);
		}

		[Test]
		public async Task LogInTest4()
		{
			
			string result = await _authenticateService.Register("NewUser", "password");
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task RegisterTest1()
		{
			string result = await _authenticateService.Register("NewUser", "password");
			Assert.IsNotNull(result);
		}

		[Test]
		public async Task RegisterTest2()
		{
			string result;
			await _authenticateService.Register("NewUser", "password");
			result = await _authenticateService.Register("NewUser", "password");
			Assert.AreEqual(result, AuthenticateStatus.AlreadyRegistered.ToString());
		}

		[TestCase("soso", "password")]
		[TestCase(null, "password")]
		[TestCase("sosssso", "pas")]
		[TestCase("sssaaoso", null)]
		[TestCase("so ssss so", "password")]
		public async Task RegisterTest2(string login, string password)
		{
			string result = await _authenticateService.Register(login, password);
			Assert.AreEqual(result, AuthenticateStatus.WrongDataFormat.ToString());
		}

		[Test]
		public async Task LogOutTest1()
		{
			ServiceLocator.Current.GetInstance<ServicesStorage>().LoggedUsers.Add("SomeKey", "SomeValue");

			bool logOutStatus = await _authenticateService.LogOut("SomeKey");
			Assert.AreEqual(logOutStatus, true);
		}

		[Test]
		public async Task LogOutTest2()
		{
			bool logOutStatus = await _authenticateService.LogOut("SomeKey");
			Assert.AreEqual(logOutStatus, false);
		}

		[Test]
		public async Task EncryptTest1()
		{
			Assert.IsNotNull(_authenticateService.Encrypt("Text"));
		}

		[Test]
		public async Task EncryptTest2()
		{
			Assert.IsNotEmpty(_authenticateService.Encrypt("Text"));
		}

		[Test]
		public async Task DecryptTest1()
		{
			string sourceText = "Hello";
			string encryptText;


			encryptText = _authenticateService.Encrypt(sourceText);

			Assert.AreEqual(sourceText, _authenticateService.Decrypt(encryptText));
		}

		[Test]
		public async Task SaveUsersTest1()
		{
			Assert.DoesNotThrowAsync(() => _authenticateService.SaveUserDataAsync());
		}

		[Test]
		public async Task LoadUsersTest1()
		{
			Assert.DoesNotThrow(() => _authenticateService.LoadUserData());
		}

	}
}
