using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DataTransferObjects;
using Northis.BattleRoostersOnline.Contracts;

namespace Northis.BattleRoostersOnline.Implements
{
	class EditService : IEditService
	{
		


		public void Add(int userID, RoosterDto rooster)
		{

		}

		public void Edit(int userID, int roosterID, RoosterDto rooster)
		{
			throw new NotImplementedException();
		}

		public void Remove(int userID, int roosterID)
		{
			throw new NotImplementedException();
		}
	}
}
