using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class PeriodFormatElement
	{
		[JsonProperty(PropertyName = "max")]
		public uint Max { get; set; }
		[JsonProperty(PropertyName = "col")]
		public uint Col { get; set; }
		[JsonProperty(PropertyName = "fontSize")]
		public int FontSize { get; set; }
		[JsonProperty(PropertyName = "row")]
		public uint Row { get; set; }
		[JsonProperty(PropertyName = "labelPattern")]
		public string LabelPattern { get; set; }
		// foreColor: color
		// backColor: color
		[JsonProperty(PropertyName = "type")]
		public uint Type { get; set; }
		[JsonProperty(PropertyName = "separator")]
		public string Separator { get; set; }
	}
}
