﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.DataStorages
{
	class RoosterFormatter
	{
		private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<RoosterDto>));

		public bool Serialize(int userID, RoosterDto rooster)
		{

			return true;
		}


	}
}
