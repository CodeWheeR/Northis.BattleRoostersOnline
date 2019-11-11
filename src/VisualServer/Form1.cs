using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using GameServer.Properties;
using NLog;
using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Contracts;
using Northis.BattleRoostersOnline.Service.DataStorages;
using Northis.BattleRoostersOnline.Service.Implements;
using Northis.BattleRoostersOnline.Service.Models;
using Unity;
using Unity.Lifetime;
using Unity.Wcf;


namespace VisualServer
{
	public partial class Form1 : Form
	{
		UnityServiceHost selfHost = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				var logger = LogManager.GetLogger("ServerLogger");

				var address = "";

				try
				{
					address = ConfigurationManager.AppSettings["baseAddress"];
				}
				catch (ConfigurationErrorsException)
				{
					logger.Error(Resources.StrErrorIPAddressNotFound);
					Logs.Text += Resources.StrErrorIPAddressNotFound + Environment.NewLine;
					return;
				}

				if (address == "")
				{
					logger.Error(Resources.StrErrorIPAddressNotFound);
					Logs.Text += Resources.StrErrorIPAddressNotFound + Environment.NewLine;
					return;
				}

				var baseAddress = new Uri(address);
				var container = new UnityContainer();
				var config = new MapperConfiguration(cfg => cfg.CreateMap<RoosterModel, RoosterDto>()
															   .IgnoreAllSourcePropertiesWithAnInaccessibleSetter());
				var mapper = config.CreateMapper();

				container.RegisterInstance(mapper);
				container.RegisterType<IDataStorageService, DataStorageService>(new ContainerControlledLifetimeManager());
				var dataStorageService = container.Resolve<IDataStorageService>();
				StatisticsPublisher.GetInstance(dataStorageService);

				container.RegisterType<IEditService, EditService>(new ContainerControlledLifetimeManager());
				container.RegisterType<IBattleService, BattleService>(new ContainerControlledLifetimeManager());
				container.RegisterType<IAuthenticateService, AuthenticateService>(new ContainerControlledLifetimeManager());

				selfHost = new UnityServiceHost(container, typeof(GameServicesProvider), baseAddress);

				try
				{
					var authBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
					{
						OpenTimeout = new TimeSpan(0, 0, 0, 5),
						SendTimeout = new TimeSpan(0, 0, 0, 5)
					};

					var editBinding = new WSHttpBinding(SecurityMode.None)
					{
						OpenTimeout = new TimeSpan(0, 0, 0, 5),
						SendTimeout = new TimeSpan(0, 0, 0, 5)
					};

					var battleBinding = new WSDualHttpBinding(WSDualHttpSecurityMode.None)
					{
						SendTimeout = new TimeSpan(0, 0, 0, 1)
					};

					selfHost.AddServiceEndpoint(typeof(IAuthenticateService), authBinding, "AuthenticationService");
					selfHost.AddServiceEndpoint(typeof(IEditService), editBinding, "EditService");
					selfHost.AddServiceEndpoint(typeof(IBattleService), battleBinding, "BattleService");

					var smb = new ServiceMetadataBehavior();
					smb.HttpGetEnabled = true;
					selfHost.Description.Behaviors.Add(smb);

					selfHost.Open();
					logger.Info(Resources.StrInfoServiceReady);
				}
				catch (CommunicationException ce)
				{
					logger.Error(Resources.StrFrmErrorCommunicationException, ce.Message);
					selfHost.Abort();
				}
			});
		}

		private void button2_Click(object sender, EventArgs e)
		{
			StatisticsPublisher.GetInstance()
							   .SendServerStopMessage("Сервер закрыт");
			selfHost.Close();
		}
	}
}
