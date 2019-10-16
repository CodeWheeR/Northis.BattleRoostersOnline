using System;
using System.Linq;
using System.Xml.Linq;
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
			XDocument document = XDocument.Load("Resources/RoosterStorage.xml");
			


		}

		public void Remove(int userID, int roosterID)
		{
			throw new NotImplementedException();
		}
	}
}
