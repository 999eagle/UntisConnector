using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using UntisLibrary.Converters;

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
		[JsonProperty(PropertyName = "foreColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color ForegroundColor { get; set; }
		[JsonProperty(PropertyName = "backColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color BackgroundColor { get; set; }
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; set; }
		[JsonProperty(PropertyName = "displayAllowed")]
		public bool DisplayAllowed { get; set; }
	}
}
