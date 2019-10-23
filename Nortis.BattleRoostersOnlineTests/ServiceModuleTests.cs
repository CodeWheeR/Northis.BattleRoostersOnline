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
		#region Protected
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
		#endregion

		#region Methods
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

				container.RegisterType<IServicesStorage, ServicesStorage>();
				container.RegisterInstance(new ServicesStorage());

				UnityServiceLocator locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
			else
			{
				var storage = ServiceLocator.Current.GetInstance<IServicesStorage>();
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
