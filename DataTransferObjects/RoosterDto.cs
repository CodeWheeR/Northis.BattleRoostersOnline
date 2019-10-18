using System.Runtime.Serialization;

namespace DataTransferObjects
{
	[DataContract]
	public class RoosterDto
    {
		public RoosterDto()
		{

		}

		[DataMember]
		public double Weight
		{
			get;
			set;
		}
		[DataMember]
		public int Height
		{
			get;
			set;
		}
		[DataMember]
		public double Health
		{
			get;
			set;
		}
		[DataMember]
		public int Stamina
		{
			get;
			set;
		}
		[DataMember]
		public RoosterColorDto ColorDto
		{
			get;
			set;
		}
		[DataMember]
		public int Brickness
		{
			get;
			set;
		}
		[DataMember]
		public CrestSizeDto Crest
		{
			get;
			set;
		}
		[DataMember]
		public int Thickness
		{
			get;
			set;
		}
		[DataMember]
		public int Luck
		{
			get;
			set;
		}
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public int WinStreak
		{
			get;
			set;
		}
		[DataMember]
		public double Hit
		{
			get;
			set;
		}
		[DataMember]
		public double Damage
		{
			get;
			set;
		}
		[DataMember]
		public int MaxHealth
		{
			get;
			set;
		}

	}
}
