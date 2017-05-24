using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class GetTimetable2017Result
	{
		[JsonProperty(PropertyName = "notModified")]
		public bool NotModified { get; set; }
		[JsonProperty(PropertyName = "timetableData")]
		public TimetableData TimetableData { get; set; }
		[JsonProperty(PropertyName = "timetableFormat")]
		public TimetableFormat TimetableFormat { get; set; }
		[JsonProperty(PropertyName = "masterData")]
		public MasterData MasterData { get; set; }
	}
}
