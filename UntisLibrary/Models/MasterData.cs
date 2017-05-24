using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class MasterData
	{
		[JsonProperty(PropertyName = "timeStamp")]
		public long TimeStamp { get; set; }
		[JsonProperty(PropertyName = "absenceReasons")]
		public AbsenceReason[] AbsenceReasons { get; set; }
		[JsonProperty(PropertyName = "departments")]
		public Department[] Departments { get; set; }
		[JsonProperty(PropertyName = "eventReasons")]
		public EventReason[] EventReasons { get; set; }
		// eventReasonGroups []
		[JsonProperty(PropertyName = "holidays")]
		public Holiday[] Holidays { get; set; }
		[JsonProperty(PropertyName = "klassen")]
		public Class[] Classes { get; set; }
		[JsonProperty(PropertyName = "rooms")]
		public Room[] Rooms { get; set; }
		[JsonProperty(PropertyName = "subjects")]
		public Subject[] Subjects { get; set; }
		[JsonProperty(PropertyName = "teachers")]
		public Teacher[] Teachers { get; set; }
		[JsonProperty(PropertyName = "timeGrid")]
		public TimeGrid TimeGrid { get; set; }
	}
}
