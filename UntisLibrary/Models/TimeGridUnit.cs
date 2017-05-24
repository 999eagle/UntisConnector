using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using UntisLibrary.Converters;

namespace UntisLibrary.Models
{
	public class TimeGridUnit
	{
		[JsonProperty(PropertyName = "label")]
		public string Label { get; set; }
		[JsonProperty(PropertyName = "startTime")]
		[JsonConverter(typeof(TimeSpanConverter))]
		public TimeSpan StartTime { get; set; }
		[JsonProperty(PropertyName = "endTime")]
		[JsonConverter(typeof(TimeSpanConverter))]
		public TimeSpan EndTime { get; set; }
	}
}
