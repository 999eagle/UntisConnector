using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class Subject
	{
		[JsonProperty(PropertyName = "id")]
		public uint ID { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "longName")]
		public string LongName { get; set; }
		[JsonProperty(PropertyName = "departmentIds")]
		public uint[] DepartmentIDs { get; set; }
		// foreColor color
		// backColor color
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; set; }
		[JsonProperty(PropertyName = "displayAllowed")]
		public bool DisplayAllowed { get; set; }
	}
}
