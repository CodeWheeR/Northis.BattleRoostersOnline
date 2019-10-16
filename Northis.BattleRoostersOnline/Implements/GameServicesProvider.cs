using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "GameServicesProvider" в коде и файле конфигурации.
	public class GameServicesProvider : IAuthenticateService, IEditService
	{
		private EditService _editService;

		private AuthenticateService _authenticateService;

		public GameServicesProvider()
		{
			_editService = new EditService();
			_authenticateService = new AuthenticateService();
			var container = new UnityContainer();
			container.RegisterInstance(new ServicesStorage());

			var locator = new UnityServiceLocator(container);
			ServiceLocator.SetLocatorProvider(() => locator);
		}

		public void Add(string userID, RoosterDto rooster) => _editService.Add(userID, rooster);

		public void Edit(string userID, int roosterSeqNum, RoosterDto rooster) => _editService.Edit(userID, roosterSeqNum, rooster);

		public void Load() => _editService.Load();

		public void Remove(string userID, int roosterID) => _editService.Remove(userID, roosterID);

		public void Save() => _editService.Save();

		public string LogIn(string login, string password) => _authenticateService.LogIn(login, password);

		public string Register(string login, string password) => _authenticateService.Register(login, password);

		public bool LogOut(string token) => _authenticateService.LogOut(token);
	}
}
