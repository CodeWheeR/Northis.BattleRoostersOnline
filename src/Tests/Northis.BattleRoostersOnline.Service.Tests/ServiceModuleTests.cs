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
		protected EditService Editor;
		/// <summary>
		/// Сервис аутентификации.
		/// </summary>
		protected AuthenticateService AuthenticateService;
		/// <summary>
		/// Сервис проведения битвы.
		/// </summary>
		protected BattleService BattleService;
		/// <summary>
		/// Callback аутентификации.
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
			get;
			set;
		}

		/// <summary>
		/// Устанавливает тестовое окружение.
		/// </summary>
		[SetUp]
		protected void Setup()
		{
			Storage = new DataStorageService();
			BaseServiceWithStorage.SetStorage(Storage);
			AuthenticateService = new AuthenticateService();
			Editor = new EditService();
			BattleService = new BattleService();

		}
		#endregion
	}
}
