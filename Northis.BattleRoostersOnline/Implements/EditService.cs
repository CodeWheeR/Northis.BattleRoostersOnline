using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;

namespace Northis.BattleRoostersOnline.Implements
{
	public class EditService : BaseServiceWithStorage, IEditService
	{
		public async void Add(string token, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login))
			{
				Storage.RoostersData[login].Add(rooster);
			}
			else
			{
				Storage.RoostersData.Add(login, new List<RoosterDto>()
				{
					rooster
				});
			}
		}

		public async Task Load()
		{
			await Task.Run(() =>
			{
				List<UserRoosters> userRoosters;

				XmlSerializer serializer = new XmlSerializer(typeof(List<UserRoosters>));

				using (FileStream fileStream = new FileStream("Resources/RoostersStorage.xml", FileMode.Open))
				{
					Storage.RoostersData.Clear();

					userRoosters = (List<UserRoosters>)serializer.Deserialize(fileStream);

					for (int i = 0; i < userRoosters.Count; i++)
					{
						Storage.RoostersData.Add(userRoosters[i].Login, userRoosters[i].Roosters.ToList());
					}
				}
			});
		}

		public async Task<IEnumerable<RoosterDto>> GetUserRoosters(string token)
		{
			return Storage.RoostersData[await GetLoginAsync(token)];
		}

		public void Save()
		{
			Task.Run(() =>
			{
				List<UserRoosters> roosters = new List<UserRoosters>();

				foreach (var val in Storage.RoostersData)
				{
					roosters.Add(new UserRoosters(val.Key, val.Value));
				}

				XmlSerializer serializer = new XmlSerializer(roosters.GetType());

				if (Directory.Exists("Resources") == false)
				{
					Directory.CreateDirectory("Resources");
				}

				using (FileStream fileStream = new FileStream("Resources\\RoostersStorage.xml", FileMode.OpenOrCreate))
				{
					serializer.Serialize(fileStream, roosters);
				}
			});
		}

		public async void Edit(string token, int roosterSeqNum, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) && Storage.RoostersData[login].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Storage.RoostersData[login.ToString()][roosterSeqNum] = rooster;
			}
		}

		public async void Remove(string token, int roosterSeqNum)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) && Storage.RoostersData[login].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Storage.RoostersData[login].RemoveAt(roosterSeqNum);
			}
		}

	}
}
