using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UntisLibrary.Models
{
	public class EventReason
	{
		[JsonProperty(PropertyName = "id")]
		public uint Id { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "longName")]
		public string LongName { get; set; }
		[JsonProperty(PropertyName = "elementType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ElementType ElementType { get; set; }
		[JsonProperty(PropertyName = "groupId")]
		public uint GroupId { get; set; }
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; set; }
	}
}
