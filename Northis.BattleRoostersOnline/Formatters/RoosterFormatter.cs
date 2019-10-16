using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Northis.BattleRoostersOnline.Formatters
{
	class RoosterFormatter
	{
		private readonly XmlSerializer _serializer = new XmlSerializer(typeof(XmlRooster));

		public bool Serialize(XmlRooster serializableData)
		{
			using (FileStream fileStream = new FileStream("Resources/RoostersStorage.xml", FileMode.Append))
			{
				_serializer.Serialize(fileStream, serializableData);
			}
			return true;
		}

	}
}
