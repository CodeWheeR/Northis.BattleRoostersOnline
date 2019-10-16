using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northis.RoosterBattle.Models;
using System.Diagnostics;
using Catel.Collections;
using Catel.Data;
using Catel.IoC;
using Catel.Runtime.Serialization;
using Catel.Runtime.Serialization.Json;
using Northis.RoosterBattle.Properties;

namespace Northis.RoosterBattle.Services
{
	/// <summary>
	/// Предоставляет механизмы сохранения и загрузки информации о петухах.
	/// </summary>
	/// <seealso cref="Northis.RoosterBattle.IRoosterKeepService" />
	class RoosterKeepService : IRoosterKeepService
	{
		#region Fields
		private readonly string _path;
		#endregion

		#region Public Methods
		public RoosterKeepService()
		{
			string directory = Directory.GetCurrentDirectory();
			if (!Directory.Exists("SaveData"))
				Directory.CreateDirectory("SaveData");
			_path = Path.Combine(directory, "SaveData\\Roosters.cock");
		}

		public void SaveRoosters(IEnumerable<RoosterModel> roosters)
		{
			var settings = new RoosterSettings();
			settings.Roosters.ReplaceRange(roosters);
			var jsonSerializer = IoCConfiguration.DefaultDependencyResolver.Resolve<IJsonSerializer>();
			settings.Save(_path, jsonSerializer);
		}

		public IEnumerable<RoosterModel> LoadRoosters()
		{
			if (!File.Exists(_path))
			{
				return new RoosterModel[] { };
			}

			using (var fileStream = File.Open(_path, FileMode.Open))
			{
				var jsonSerializer = IoCConfiguration.DefaultDependencyResolver.Resolve<IJsonSerializer>();
				var settings = RoosterSettings.Load(fileStream, jsonSerializer);
				var roosters = new RoosterModel[settings.Roosters.Count];
				for (int i = 0; i < roosters.Length; i++)
				{
					roosters[i] = (RoosterModel)settings.Roosters[i]
										  .Clone();
				}

				return roosters;
			}

		}

		#region Async
		public Task<IEnumerable<RoosterModel>> LoadRoostersAsync() => Task.Run(() => LoadRoosters());

		public Task SaveRoostersAsync(IEnumerable<RoosterModel> roosters) => Task.Run(() => SaveRoosters(roosters));
		#endregion

		#endregion
	}
}
