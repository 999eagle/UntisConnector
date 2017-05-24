using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class TimeGrid
	{
		[JsonProperty(PropertyName = "days")]
		public TimeGridDay[] Days { get; set; }
	}
}
