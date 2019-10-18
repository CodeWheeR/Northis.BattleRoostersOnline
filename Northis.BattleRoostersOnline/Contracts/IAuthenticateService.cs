using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using Microsoft.SqlServer.Server;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract(SessionMode = SessionMode.Required)]
	public interface IAuthenticateService
	{
		[OperationContract(IsInitiating = true)]
		Task<string> LogIn(string login, string password);
		[OperationContract(IsInitiating = true)]
		Task<string> Register(string login, string password);
		[OperationContract(IsTerminating = true)]
		Task<bool> LogOut(string token);
		[OperationContract]
		AuthenticateStatus GetLoginStatus();
	}
}
