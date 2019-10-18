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
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
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

		public void Add(string token, RoosterDto rooster) => Task.Run(() => _editService.Add(token, rooster));

		public void Edit(string token, int roosterId, RoosterDto rooster) => Task.Run(() => _editService.Edit(token, roosterId, rooster));

		public async Task<IEnumerable<RoosterDto>> GetUserRoosters(string token) => await _editService.GetUserRoosters(token);

		public void Remove(string token, int roosterId) => _editService.Remove(token, roosterId);

		public async Task<string> LogIn(string login, string password) =>  await _authenticateService.LogIn(login, password);

		public async Task<string> Register(string login, string password) => await _authenticateService.Register(login, password);

		public async Task<bool> LogOut(string token) => await _authenticateService.LogOut(token);

		public AuthenticateStatus GetLoginStatus() => _authenticateService.GetLoginStatus();

		public void FindMatch(string token, RoosterDto rooster) => Task.Run(() => _findService.FindMatch(token, rooster));

		public bool CancelFinding(string token) => _findService.CancelFinding(token);

		public void StartBattle(string token, string matchToken) => Task.Run(() => _battleService.StartBattle(token, matchToken));

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

