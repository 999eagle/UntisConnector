using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UntisLibrary.Models
{
	public class PeriodElement
	{
		[JsonProperty(PropertyName = "type")]
		public ElementType ElementType { get; set; }
		[JsonProperty(PropertyName = "id")]
		public uint ElementId { get; set; }
		[JsonProperty(PropertyName = "orgId")]
		public uint OrgId { get; set; }
		[JsonProperty(PropertyName = "missing")]
		public bool Missing { get; set; }
		[JsonProperty(PropertyName = "state")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PeriodElementState State { get; set; }
	}
}
