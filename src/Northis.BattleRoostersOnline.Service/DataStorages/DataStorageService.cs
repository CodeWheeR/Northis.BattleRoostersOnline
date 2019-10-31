using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using CommonServiceLocator;
using Unity;
using Unity.ServiceLocation;

using Northis.BattleRoostersOnline.Dto;
using Northis.BattleRoostersOnline.Service.Implements;
using Northis.BattleRoostersOnline.Service.Models;

namespace Northis.BattleRoostersOnline.Service.DataStorages
{
	/// <summary>
	/// Класс, инкапсулирующий в себе данные о пользователях, петухах, авторизированных пользователях, игровых сессиях.
	/// </summary>
	[Serializable]
	public class DataStorageService : BaseService, IDataStorageService
	{
		#region Fields
		/// <summary>
		/// Бинарный сериализатор
		/// </summary>
		private readonly BinaryFormatter _formatter = new BinaryFormatter();
		/// <summary>
		/// Токен отмены операции мониторинга подключений.
		/// </summary>
		private CancellationTokenSource _connectionMonitorTokenSource = new CancellationTokenSource();

		private object _RoostersFileLocker = new object();
		private object _UsersFileLocker = new object();
		#endregion
		
		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DataStorageService" /> класса.
		/// </summary>
		public DataStorageService()
		{
			UserData = new Dictionary<string, string>();
			RoostersData = new Dictionary<string, Dictionary<string, RoosterModel>>();
			LoggedUsers = new Dictionary<string, string>();
			Sessions = new Dictionary<string, Session>();
			Task.Run(() => InitContent());
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Возвращает или задает данные пользователя.
		/// </summary>
		/// <value>
		/// Данные пользователя.
		/// </value>
		public Dictionary<string, string> UserData
		{
			get;
			private set;
		}

		/// <summary>
		/// Возвращает или задает данные петухов.
		/// </summary>
		/// <value>
		/// Данные петухов.
		/// </value>
		public Dictionary<string, Dictionary<string,RoosterModel>> RoostersData
		{
			get;
		}

		/// <summary>
		/// Возвращает или задает данные об авторизированных пользователях.
		/// </summary>
		/// <value>
		/// Авторизированные пользователи.
		/// </value>
		public Dictionary<string, string> LoggedUsers
		{
			get;
		}

		/// <summary>
		/// Возвращает или задает данные об игровых сессиях.
		/// </summary>
		/// <value>
		/// Игровые сессии.
		/// </value>
		public Dictionary<string, Session> Sessions
		{
			get;
		}
		#endregion

		#region Private Methods

		private async Task MonitorConnections()
		{
			var token = _connectionMonitorTokenSource.Token;

			await Task.Run(async () =>
			{
				while (!token.IsCancellationRequested)
				{
					StatisticsPublisher.GetInstance()
									   .UpdateStatistics();
					await Task.Delay(10000, token);
				}
			}, token);
		}

		private void InitContent()
		{
			LoadUserData();
			LoadRoosters();

			#pragma warning disable 4014
			MonitorConnections();
			#pragma warning restore 4014
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Асинхронно сохраняет петухов.
		/// </summary>
		public async Task SaveRoostersAsync()
		{
			await Task.Run(() =>
			{
				var roosters = new List<UserRoosters>();

				lock (RoostersData)
				{
					foreach (var val in RoostersData)
					{
						roosters.Add(new UserRoosters(val.Key, val.Value.Values));
					}
				}

				var serializer = new DataContractSerializer(roosters.GetType());

				if (Directory.Exists("Resources") == false)
				{
					Directory.CreateDirectory("Resources");
				}

				lock (_RoostersFileLocker)
				{
					using (var fileStream = new FileStream("Resources\\RoostersStorage.xml", FileMode.Create))
					{
						serializer.WriteObject(fileStream, roosters);
					}
				}
			});
		}


		/// <summary>
		/// Загружает петухов.
		/// </summary>
		public void LoadRoosters()
		{
			List<UserRoosters> userRoosters;
			var serializer = new DataContractSerializer(typeof(List<UserRoosters>));

			if (File.Exists("Resources\\RoostersStorage.xml"))
			{
				try
				{
					lock (_RoostersFileLocker)
					{
						using (var fileStream = new FileStream("Resources\\RoostersStorage.xml", FileMode.Open))
						{
							userRoosters = (List<UserRoosters>) serializer.ReadObject(fileStream);
						}
					}
				}
				catch (SerializationException e)
				{
					userRoosters = new List<UserRoosters>();
				}

				lock (RoostersData)
				{
					RoostersData.Clear();

					foreach (var roosters in userRoosters)
					{
						RoostersData.Add(roosters.Login, new Dictionary<string, RoosterModel>());
						foreach (var rooster in roosters.Roosters)
						{
							if (!string.IsNullOrWhiteSpace(rooster.Token))
							{
								RoostersData[roosters.Login].Add(rooster.Token, rooster);
							}
							else
							{
								RoostersData[roosters.Login].Add(GenerateToken(RoostersData[roosters.Login].ContainsKey), rooster);
							}
						}
					}
				}
				
			}

		}

		/// <summary>
		/// Асинхронно сохраняет данные пользователей.
		/// </summary>
		public async Task SaveUserDataAsync()
		{
			await Task.Run(() =>
			{
				if (!Directory.Exists("Resources"))
				{
					Directory.CreateDirectory("Resources");
				}

				lock (_UsersFileLocker)
				{
					using (var fs = new FileStream("Resources\\users.dat", FileMode.Create))
					{
						_formatter.Serialize(fs, UserData);
					}
				}
			});
		}

		/// <summary>
		/// Загружает данные пользователей.
		/// </summary>
		public void LoadUserData()
		{
			if (File.Exists("Resources\\users.dat"))
			{
				lock (_UsersFileLocker)
				{
					using (var fs = new FileStream("Resources\\users.dat", FileMode.Open))
					{
						UserData = (Dictionary<string, string>)_formatter.Deserialize(fs);
					}
				}
			}
		}
		#endregion

		public static void InitContainer()
		{
			if (!ServiceLocator.IsLocationProviderSet)
			{
				var container = new UnityContainer();
				container.RegisterType<IDataStorageService, DataStorageService>();
				container.RegisterInstance(new DataStorageService());

				var locator = new UnityServiceLocator(container);
				ServiceLocator.SetLocatorProvider(() => locator);
			}
		}

	}
}
