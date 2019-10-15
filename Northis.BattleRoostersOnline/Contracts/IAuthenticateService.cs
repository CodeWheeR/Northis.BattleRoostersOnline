using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Northis.BattleRoostersOnline.Contracts
{
	[ServiceContract]
	interface IAuthenticateService
	{
		[OperationContract]
		string LogIn(string login, string password);
		[OperationContract]
		string Register(string login, string password, string codePhrase);
		[OperationContract(IsTerminating = true)]
		bool LogOut();
		[OperationContract]
		string ResetPassword(string username, string codePhrase);
	}
}
