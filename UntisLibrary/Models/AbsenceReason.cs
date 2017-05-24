using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class AbsenceReason
	{
		[JsonProperty(PropertyName = "id")]
		public uint Id { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "longName")]
		public string LongName { get; set; }
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; set; }
	}
}
