﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DataTransferObjects;
using Northis.BattleRoostersOnline.DataStorages;

namespace Northis.BattleRoostersOnline.Models
{
	/// <summary>
	/// Класс, инкапсулирующий в себе данные о пользователях, петухах, авторизированных пользователях, игровых сессиях.
	/// </summary>
	[Serializable]
	public class ServicesStorage : IServicesStorage
	{
		/// <summary>
		/// Бинарный сериализатор
		/// </summary>
		private readonly BinaryFormatter _formatter = new BinaryFormatter();


		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ServicesStorage" /> класса.
		/// </summary>
		public ServicesStorage()
		{
			UserData = new Dictionary<string, string>();
			RoostersData = new Dictionary<string, List<RoosterDto>>();
			LoggedUsers = new Dictionary<string, string>();
			Sessions = new Dictionary<string, Session>();
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
		public Dictionary<string, List<RoosterDto>> RoostersData
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
						roosters.Add(new UserRoosters(val.Key, val.Value));
					}
				}

				var serializer = new DataContractSerializer(roosters.GetType());

				if (Directory.Exists("Resources") == false)
				{
					Directory.CreateDirectory("Resources");
				}

				using (var fileStream = new FileStream("Resources\\RoostersStorage.xml", FileMode.OpenOrCreate))
				{
					serializer.WriteObject(fileStream, roosters);
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
				using (var fileStream = new FileStream("Resources/RoostersStorage.xml", FileMode.Open))
				{
					RoostersData.Clear();

					userRoosters = (List<UserRoosters>)serializer.ReadObject(fileStream);

					lock (RoostersData)
					{
						for (var i = 0; i < userRoosters.Count; i++)
						{
							RoostersData.Add(userRoosters[i]
														 .Login,
													 userRoosters[i]
														 .Roosters.ToList());
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

				using (var fs = new FileStream("Resources\\users.dat", FileMode.OpenOrCreate))
				{
					_formatter.Serialize(fs, UserData);
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
				using (var fs = new FileStream("Resources\\users.dat", FileMode.Open))
				{
					UserData = (Dictionary<string, string>)_formatter.Deserialize(fs);
				}
			}
		}
	}
}
