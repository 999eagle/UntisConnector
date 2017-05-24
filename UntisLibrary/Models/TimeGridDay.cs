using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UntisLibrary.Converters;

namespace UntisLibrary.Models
{
	public class TimeGridDay
	{
		[JsonProperty(PropertyName = "day")]
		[JsonConverter(typeof(DayOfWeekConverter))]
		public DayOfWeek Day { get; set; }
		[JsonProperty(PropertyName = "units")]
		public TimeGridUnit[] Units { get; set; }
	}
}
