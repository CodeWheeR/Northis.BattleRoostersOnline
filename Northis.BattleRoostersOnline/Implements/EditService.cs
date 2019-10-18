using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;

namespace Northis.BattleRoostersOnline.Implements
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
	public class EditService : BaseServiceWithStorage, IEditService
	{
		public async Task Add(string token, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);

			await Task.Run(() =>
			{
				if (Storage.RoostersData.ContainsKey(login))
				{
					lock (Storage.RoostersData)
					{
						Storage.RoostersData[login]
							   .Add(rooster);
					}
				}
				else
				{
					lock (Storage.RoostersData)
					{
						Storage.RoostersData.Add(login,
												 new List<RoosterDto>
												 {
													 rooster
												 });
					}
				}
			}).ConfigureAwait(false);
		}

		public void Load()
		{
			List<UserRoosters> userRoosters;
			var serializer = new XmlSerializer(typeof(List<UserRoosters>));

			using (var fileStream = new FileStream("Resources/RoostersStorage.xml", FileMode.Open))
			{
				Storage.RoostersData.Clear();

				userRoosters = (List<UserRoosters>) serializer.Deserialize(fileStream);

				lock (Storage.RoostersData)
				{
					for (var i = 0; i < userRoosters.Count; i++)
					{
						Storage.RoostersData.Add(userRoosters[i]
													 .Login,
												 userRoosters[i]
													 .Roosters.ToList());
					}
				}
			}
		}

		public async Task<IEnumerable<RoosterDto>> GetUserRoosters(string token)
		{
			var login = await GetLoginAsync(token);

			return await Task.Run<IEnumerable<RoosterDto>>(() =>
			{
				if (Storage.RoostersData.ContainsKey(login))
				{
					return Storage.RoostersData[login];
				}

				return new List<RoosterDto>();
			}).ConfigureAwait(false);
		}

		public void Save()
		{
			Task.Run(() =>
			{
				var roosters = new List<UserRoosters>();

				lock (Storage.RoostersData)
				{
					foreach (var val in Storage.RoostersData)
					{
						roosters.Add(new UserRoosters(val.Key, val.Value));
					}
				}

				var serializer = new XmlSerializer(roosters.GetType());

				if (Directory.Exists("Resources") == false)
				{
					Directory.CreateDirectory("Resources");
				}

				using (var fileStream = new FileStream("Resources\\RoostersStorage.xml", FileMode.OpenOrCreate))
				{
					serializer.Serialize(fileStream, roosters);
				}
			});
		}

		public async Task Edit(string token, int roosterSeqNum, RoosterDto rooster)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) &&
				Storage.RoostersData[login]
					   .Count >
				roosterSeqNum &&
				roosterSeqNum >= 0)
			{
				lock (Storage.RoostersData)
				{
					Storage.RoostersData[login][roosterSeqNum] = rooster;
				}
			}
		}

		public async Task Remove(string token, int roosterSeqNum)
		{
			var login = await GetLoginAsync(token);
			if (Storage.RoostersData.ContainsKey(login) &&
				Storage.RoostersData[login]
					   .Count >
				roosterSeqNum &&
				roosterSeqNum >= 0)
			{
				lock (Storage.RoostersData)
				{
					Storage.RoostersData[login]
						   .RemoveAt(roosterSeqNum);
				}
			}
		}
	}
}
