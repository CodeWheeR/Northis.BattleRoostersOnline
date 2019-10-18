using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.Models;
using Unity;
using Unity.ServiceLocation;

namespace Northis.BattleRoostersOnline.Implements
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "GameServicesProvider" в коде и файле конфигурации.
	public class GameServicesProvider : IAuthenticateService, IEditService, IFindService, IBattleService
	{
		private EditService _editService;
		private FindService _findService;
		private AuthenticateService _authenticateService;
		private BattleService _battleService;

		public GameServicesProvider()
		{
			_editService = new EditService();
			_authenticateService = new AuthenticateService();
			_findService = new FindService();
			_battleService = new BattleService();
		}

		public void Add(string login, RoosterDto rooster) => _editService.Add(login, rooster);

		public void Edit(string login, int roosterSeqNum, RoosterDto rooster) => _editService.Edit(login, roosterSeqNum, rooster);

		public async Task Load() => await _editService.Load();

		public Task<IEnumerable<RoosterDto>> GetUserRoosters(string token) => _editService.GetUserRoosters(token);

		public void Remove(string token, int roosterID) => _editService.Remove(token, roosterID);

		public void Save() => _editService.Save();

		public async Task<string> LogIn(string login, string password) =>  await _authenticateService.LogIn(login, password);

		public async Task<string> Register(string login, string password) => await _authenticateService.Register(login, password);

		public bool LogOut(string token) => _authenticateService.LogOut(token);

		public void FindMatch(string token, RoosterDto rooster) => _findService.FindMatch(token, rooster);

		public bool CancelFinding(string token) => _findService.CancelFinding(token);

		public void StartBattle(string token, string matchToken) => _battleService.StartBattle(token, matchToken);

		public void Beak(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void Bite(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void Pull(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public void GiveUp(string token, string matchToken)
		{
			throw new NotImplementedException();
		}
	}
}

