using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Northis.BattleRoostersOnline.Service.Extensions
{
	class DdosDefender
	{
		private Dictionary<string, AuthState> _connectionsMonitor = new Dictionary<string, AuthState>(30);

		#region Inner
		private class AuthState
		{
			/// <summary>
			/// Предыдущая попытка подключения.
			/// </summary>
			public DateTime PreviousConnect = DateTime.MinValue;
			/// <summary>
			/// Счетчик попыток подключения в сессии из 3 попыток.
			/// </summary>
			public int Repeats = 0;
			/// <summary>
			/// Счётчик неудачных сессий подключений из 3 попыток.
			/// </summary>
			public int UnsuccesfulRepeats = 0;

			public DateTime DeniedTime = DateTime.MinValue;
		}
		#endregion
		/// <summary>
		/// Осуществляет проверку на ddos - атаки с клиентского ip - адреса.
		/// </summary>
		/// <returns>true, если осуществляется ddos - атака, иначе - false.</returns>
		public bool CheckAgressiveConnection(out int waitTime)
		{
			OperationContext opContext = OperationContext.Current;

			MessageProperties properties = opContext.IncomingMessageProperties;

			RemoteEndpointMessageProperty messageProperty = (RemoteEndpointMessageProperty) properties[RemoteEndpointMessageProperty.Name];


			string ipAddress = messageProperty.Address;

			if (_connectionsMonitor.ContainsKey(ipAddress) == false)
			{
				_connectionsMonitor.Add(ipAddress, new AuthState());
				_connectionsMonitor[ipAddress].PreviousConnect = DateTime.Now;
			}

			if (DateTime.Now.Subtract(_connectionsMonitor[ipAddress]
										  .PreviousConnect)
						.TotalMinutes >
				10)
			{
				_connectionsMonitor[ipAddress]
					.UnsuccesfulRepeats = 0;
			}

			else if (_connectionsMonitor[ipAddress]
						 .Repeats >=
					 3)
			{
				_connectionsMonitor[ipAddress]
					.UnsuccesfulRepeats++;
				_connectionsMonitor[ipAddress]
					.Repeats = 0;
				_connectionsMonitor[ipAddress].DeniedTime = DateTime.Now.AddMinutes(_connectionsMonitor[ipAddress].UnsuccesfulRepeats);
				waitTime = (int) _connectionsMonitor[ipAddress]
						   .DeniedTime.Subtract(DateTime.Now)
						   .TotalSeconds;
				return true;
			}
			else if (DateTime.Now <=
					 _connectionsMonitor[ipAddress]
						 .DeniedTime)
			{
				waitTime = (int)_connectionsMonitor[ipAddress]
								.DeniedTime.Subtract(DateTime.Now)
								.TotalSeconds;
				return true;
			}
			else if (DateTime.Now.Subtract(_connectionsMonitor[ipAddress].PreviousConnect).TotalSeconds < 10)
			{
				_connectionsMonitor[ipAddress].Repeats++;
			}
			else if (DateTime.Now.Subtract(_connectionsMonitor[ipAddress].PreviousConnect).TotalSeconds >= 10)
			{
				_connectionsMonitor[ipAddress].Repeats = 0;
			}
			_connectionsMonitor[ipAddress].PreviousConnect = DateTime.Now;
			waitTime = 0;
			return false;
		}

	}
}
