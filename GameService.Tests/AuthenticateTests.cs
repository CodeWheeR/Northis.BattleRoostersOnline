﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CommonServiceLocator;
using DataTransferObjects;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Enums;

namespace Northis.BattleRoostersOnline.GameService.Tests
{
	[TestFixture]
	public class AuthenticateTests
	{
		private AuthenticateService authService;

		[SetUp]
		public void Setup()
		{
			authService = new AuthenticateService();
			var container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage()
			{
				UserData = new Dictionary<string, string>()
				{
					{"Вася Пупкин", authService.Encrypt("asdshka")},
					{"DengiVZaim",  authService.Encrypt("88005553535")}
				},

				RoosterData = new Dictionary<string, List<RoosterDto>>()
				{
					{"123oijhokjuh1256", new List<RoosterDto>(1)}
				}

			});

			var locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}
		[TestCase("Вася Пупкин", "asdshka")]
		[TestCase("DengiVZaim", "88005553535")]
		public void LogInTest(string login, string password)
		{
			Assert.AreNotEqual(authService.LogIn(login, password), AuthenticateStatus.WrongLoginOrPassword.ToString());
		}


		[TestCase("lesyabulbulyator")]
		[TestCase("asdghjlojhj87687643jkbkjmjk2")]
		public void CryptTest(string origin)
		{
			var encrypted = authService.Encrypt(origin);
			var decrypted = authService.Decrypt(encrypted);

			Assert.AreEqual(origin, decrypted);
		}
		[Test]
		public void SaveTest()
		{
			authService.SaveUserData();
			var res = authService.LoadUserData();
			Assert.AreEqual(res, ServiceLocator.Current.GetInstance<ServicesStorage>().UserData);
		}

		[TestCase("123oijhokjuh1256")]
		public void LogOutTest(string token)
		{
			var colSize = ServiceLocator.Current.GetInstance<ServicesStorage>()
										.RoosterData.Count();
			authService.LogOut(token);
			Assert.AreEqual(colSize, ServiceLocator.Current.GetInstance<ServicesStorage>()
												   .RoosterData.Count()+1);
		}

		[TestCase("Уася Лошков", "Нагибатор228тут")]
		public void RegisterTest(string login, string password)
		{
			authService.Register(login, password);
			Assert.True(((Dictionary<string, string>)ServiceLocator.Current.GetInstance<ServicesStorage>()
																   .UserData).ContainsKey(login));
			var encryptedPassword = ((Dictionary<string, string>) ServiceLocator.Current.GetInstance<ServicesStorage>()
																	  .UserData)[login];
			Assert.AreEqual(encryptedPassword, authService.Encrypt(password));
		}
	}
}