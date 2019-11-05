using System;
using CommonServiceLocator;
using Moq;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Implements;
using Northis.BattleRoostersOnline.Service.Models;
using NUnit.Framework;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Service.Tests
{
	/// <summary>
	/// Настраивает тестовое окружение и содержит реализации сервисов и Callbacks к ним.
	/// </summary>
	public class ServiceModuleTests
	{
		#region Fields
		/// <summary>
		/// Сервис редактирования.
		/// </summary>
		protected EditService Editor = new EditService();
		/// <summary>
		/// Сеовис аунтефикации.
		/// </summary>
		protected AuthenticateService AuthenticateService = new AuthenticateService();
		/// <summary>
		/// Сервис проведения битвы.
		/// </summary>
		protected BattleService BattleService = new BattleService();
		/// <summary>
		/// Callback аунтефикации.
		/// </summary>
		protected Mock<IAuthenticateServiceCallback> CallbackAuth = new Mock<IAuthenticateServiceCallback>();
		/// <summary>
		/// Callback битв.
		/// </summary>
		protected Mock<IBattleServiceCallback> callbackBattle = new Mock<IBattleServiceCallback>();
        #endregion

        #region Properties        
        /// <summary>
        /// Получает хранилище игровых данных.
        /// </summary>
        /// <value>
        /// Хранилище игровых данных.
        /// </value>
        /// <exception cref="NullReferenceException">Хранилище данных не инициализированно.</exception>
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
	}
}
