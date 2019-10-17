using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CommonServiceLocator;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;
using Northis.BattleRoostersOnline.DataStorages;

namespace Northis.BattleRoostersOnline.Implements
{
	public class EditService : BaseServiceWithStorage, IEditService
	{
		public void Add(string userID, RoosterDto rooster)
		{
			if (Storage.RoostersData.ContainsKey(userID))
			{
				Storage.RoostersData[userID].Add(rooster);
			}
			else
			{
				Storage.RoostersData.Add(userID, new List<RoosterDto>()
				{
					rooster
				});
			}
		}

		public void Load()
		{
			List<UserRoosters> userRoosters = null;

			XmlSerializer serializer = new XmlSerializer(typeof(List<UserRoosters>));

			using (FileStream fileStream = new FileStream("Resources/RoostersStorage.xml", FileMode.Open))
			{
				Storage.RoostersData.Clear();

				userRoosters = (List<UserRoosters>) serializer.Deserialize(fileStream);

				for (int i = 0; i < userRoosters.Count; i++)
				{
					Storage.RoostersData.Add(userRoosters[i].ID, userRoosters[i].roosters.ToList());
				}
			}
		}

		public IEnumerable<RoosterDto> GetUserRoosters(string token)
		{
			return Storage.RoostersData[Storage.LoggedUsers[token]];
		}

		public void Save()
		{
			List<UserRoosters> roosters = new List<UserRoosters>();

			foreach (var val in Storage.RoostersData)
			{
				roosters.Add(new UserRoosters(val));
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
		}

		public void Edit(string userID, int roosterSeqNum, RoosterDto rooster)
		{
			if (Storage.RoostersData.ContainsKey(userID) && Storage.RoostersData[userID].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Storage.RoostersData[userID.ToString()][roosterSeqNum] = rooster;
			}
		}

		public void Remove(string userID, int roosterSeqNum)
		{
			if (Storage.RoostersData.ContainsKey(userID) && Storage.RoostersData[userID].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Storage.RoostersData[userID].RemoveAt(roosterSeqNum);
			}
		}
	}
}
