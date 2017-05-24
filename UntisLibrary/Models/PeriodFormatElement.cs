using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using UntisLibrary.Converters;

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
		[JsonProperty(PropertyName = "foreColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color ForegroundColor { get; set; }
		[JsonProperty(PropertyName = "backColor")]
		[JsonConverter(typeof(ColorConverter))]
		public Color BackgroundColor { get; set; }
		[JsonProperty(PropertyName = "type")]
		public ElementType Type { get; set; }
		[JsonProperty(PropertyName = "separator")]
		public string Separator { get; set; }
	}
}
