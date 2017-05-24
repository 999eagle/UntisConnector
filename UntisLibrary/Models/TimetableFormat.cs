using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using UntisLibrary.Converters;

namespace UntisLibrary.Models
{
	public class TimetableFormat
	{
		[JsonProperty(PropertyName = "startTime")]
		[JsonConverter(typeof(TimeSpanConverter), true)]
		public TimeSpan StartTime { get; set; }
		[JsonProperty(PropertyName = "endTime")]
		[JsonConverter(typeof(TimeSpanConverter), true)]
		public TimeSpan EndTime { get; set; }
		[JsonProperty(PropertyName = "showBreakSupervisions")]
		public bool ShowBreakSupervisions { get; set; }
		[JsonProperty(PropertyName = "timegridDays")]
		public bool TimegridDays { get; set; }
		[JsonProperty(PropertyName = "connectConsecutive")]
		public bool ConnectConsecutive { get; set; }
		[JsonProperty(PropertyName = "colHeads")]
		public uint ColHeads { get; set; }
		[JsonProperty(PropertyName = "showLegend")]
		public bool ShowLegend { get; set; }
		[JsonProperty(PropertyName = "hideDetails")]
		public bool HideDetails { get; set; }
		[JsonProperty(PropertyName = "colHeads0")]
		public uint ColHeads0 { get; set; }
		[JsonProperty(PropertyName = "longName")]
		public string LongName { get; set; }
		[JsonProperty(PropertyName = "rowHeads")]
		public uint RowHeads { get; set; }
		[JsonProperty(PropertyName = "days")]
		public uint Days { get; set; }
		[JsonProperty(PropertyName = "maxSlices")]
		public uint MaxSlices { get; set; }
		[JsonProperty(PropertyName = "showHorizontalGridLines")]
		public bool ShowHorizontalGridLines { get; set; }
		[JsonProperty(PropertyName = "hideEmptyColumns")]
		public bool HideEmptyColumns { get; set; }
		// elementInfo: null
		[JsonProperty(PropertyName = "periodFormat")]
		public PeriodFormat PeriodFormat { get; set; }
		[JsonProperty(PropertyName = "renderOnServer")]
		public bool RenderOnServer { get; set; }
		[JsonProperty(PropertyName = "renderRealTime")]
		public bool RenderRealTime { get; set; }
		[JsonProperty(PropertyName = "periodConnectType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PeriodConnectType PeriodConnectType { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "id")]
		public uint Id { get; set; }
		[JsonProperty(PropertyName = "type")]
		public uint Type { get; set; }
	}
}
