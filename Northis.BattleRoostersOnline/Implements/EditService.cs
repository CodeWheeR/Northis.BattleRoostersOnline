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
	public class EditService : IEditService
	{
		public Dictionary<string, List<RoosterDto>> Roosters
		{
			get
			{
				if (ServiceLocator.IsLocationProviderSet)
				{
					return ServiceLocator.Current.GetInstance<ServicesStorage>()
										 .RoosterData;
				}
				else
				{
					throw new NullReferenceException("Storage is null");
				}

			}

		}

		public void Add(string userID, RoosterDto rooster)
		{
			if (Roosters.ContainsKey(userID))
			{
				Roosters[userID].Add(rooster);
			}
			else
			{
				Roosters.Add(userID, new List<RoosterDto>()
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
				Roosters.Clear();

				userRoosters = (List<UserRoosters>) serializer.Deserialize(fileStream);

				for (int i = 0; i < userRoosters.Count; i++)
				{
					Roosters.Add(userRoosters[i].ID, userRoosters[i].roosters.ToList());
				}
			}
		}

		public void Save()
		{
			List<UserRoosters> roosters = new List<UserRoosters>();

			foreach (var val in Roosters)
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
			if (Roosters.ContainsKey(userID) && Roosters[userID].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Roosters[userID.ToString()][roosterSeqNum] = rooster;
			}
		}

		public void Remove(string userID, int roosterSeqNum)
		{
			if (Roosters.ContainsKey(userID) && Roosters[userID].Count > roosterSeqNum && roosterSeqNum >= 0)
			{
				Roosters[userID].RemoveAt(roosterSeqNum);
			}
		}
	}
}
