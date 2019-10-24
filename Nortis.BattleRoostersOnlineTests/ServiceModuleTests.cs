using System;
using CommonServiceLocator;
using Moq;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Northis.BattleRoostersOnline.Implements;
using Northis.BattleRoostersOnline.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;

namespace Nortis.BattleRoostersOnlineTests
{
	/// <summary>
	/// Настраивает тестовое окружение и содержит реализации сервисов и Callback-ов к ним.
	/// </summary>
	public class ServiceModuleTests
	{
		#region Fields
		/// <summary>
		/// Сервис редактирования.
		/// </summary>
		protected EditService editor = new EditService();
		/// <summary>
		/// Сеовис аунтефикации.
		/// </summary>
		protected AuthenticateService authenticateService = new AuthenticateService();
		/// <summary>
		/// Сервис проведения битвы.
		/// </summary>
		protected BattleService battleService = new BattleService();
		/// <summary>
		/// Callback аунтефикации.
		/// </summary>
		protected Mock<IAuthenticateServiceCallback> callbackAuth = new Mock<IAuthenticateServiceCallback>();
		/// <summary>
		/// Callback битв.
		/// </summary>
		protected Mock<IBattleServiceCallback> callbackBattle = new Mock<IBattleServiceCallback>();
		#endregion

		#region Methods

		protected IDataStorageService Storage
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
				{
					return ServiceLocator.Current.GetInstance<IDataStorageService>();
				}

				throw new NullReferenceException("StorageService is null");
			}
		}

		#region Protected
		/// <summary>
		/// Устанавливает тестовое окружение.
		/// </summary>
		[SetUp]
		protected void SetupServiceLocator()
		{
			if (ServiceLocator.IsLocationProviderSet == false)
			{
				UnityContainer container = new UnityContainer();

				container.RegisterType<IDataStorageService, DataStorageService>();
				container.RegisterInstance(new DataStorageService());

				UnityServiceLocator locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
			else
			{
				var storage = ServiceLocator.Current.GetInstance<IDataStorageService>();
				storage.LoggedUsers.Clear();
				storage.RoostersData.Clear();
				storage.Sessions.Clear();
				storage.UserData.Clear();
			}
		}
		#endregion
		#endregion
	}
}
