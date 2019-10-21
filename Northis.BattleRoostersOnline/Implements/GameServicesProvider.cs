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
	public class GameServicesProvider : IAuthenticateService, IEditService, IBattleService
	{
		private EditService _editService;
		private AuthenticateService _authenticateService;
		private BattleService _battleService;

		public GameServicesProvider()
		{
			_editService = new EditService();
			_authenticateService = new AuthenticateService();
			_battleService = new BattleService();
		}

		public async Task Add(string token, RoosterDto rooster) => await _editService.Add(token, rooster);

		public async Task EditAsync(string token, int roosterId, RoosterDto rooster) => await _editService.EditAsync(token, roosterId, rooster);

		public async Task<IEnumerable<RoosterDto>> GetUserRoosters(string token) => await _editService.GetUserRoosters(token);

		public async Task RemoveAsync(string token, int roosterId) => _editService.RemoveAsync(token, roosterId);

		public async Task<string> LogIn(string login, string password) =>  await _authenticateService.LogIn(login, password);

		public async Task<string> Register(string login, string password) => await _authenticateService.Register(login, password);

		public async Task<bool> LogOut(string token) => await _authenticateService.LogOut(token);

		public AuthenticateStatus GetLoginStatus() => _authenticateService.GetLoginStatus();

		public async Task FindMatch(string token, RoosterDto rooster) => _battleService.FindMatch(token, rooster);

		public bool CancelFinding(string token) => _battleService.CancelFinding(token);

		public async Task StartBattle(string token, string matchToken) =>  _battleService.StartBattle(token, matchToken);

		public async Task Beak(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public async Task Bite(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public async Task Pull(string token, string matchToken)
		{
			throw new NotImplementedException();
		}

		public async Task GiveUp(string token, string matchToken) => await _battleService.GiveUp(token, matchToken);
	}
}

