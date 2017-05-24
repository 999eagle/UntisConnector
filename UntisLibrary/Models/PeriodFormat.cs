using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class PeriodFormat
	{
		[JsonProperty(PropertyName = "rows")]
		public uint Rows { get; set; }
		[JsonProperty(PropertyName = "timePosition")]
		public Position TimePosition { get; set; }
		[JsonProperty(PropertyName = "cols")]
		public uint Cols { get; set; }
		[JsonProperty(PropertyName = "textPosition")]
		public Position TextPosition { get; set; }
		[JsonProperty(PropertyName = "infoPosition")]
		public Position InfoPosition { get; set; }
		[JsonProperty(PropertyName = "userPosition")]
		public Position UserPosition { get; set; }
		[JsonProperty(PropertyName = "rescheduleInfoPosition")]
		public Position RescheduleInfoPosition { get; set; }
		[JsonProperty(PropertyName = "showSubstElements")]
		public bool ShowSubstElements { get; set; }
		[JsonProperty(PropertyName = "substituteTextForEmptySubject")]
		public bool SubstitueTextForEmptySubject { get; set; }
		[JsonProperty(PropertyName = "ignoreIndividualColors")]
		public bool IgnoreIndividualColors { get; set; }
		[JsonProperty(PropertyName = "minHeight")]
		public uint MinHeight { get; set; }
		[JsonProperty(PropertyName = "minWidth")]
		public uint MinWidth { get; set; }
		[JsonProperty(PropertyName = "elements")]
		public PeriodFormatElement[] Elements { get; set; }

		public class Position
		{
			[JsonProperty(PropertyName = "bottom")]
			public bool Bottom { get; set; }
			[JsonProperty(PropertyName = "top")]
			public bool Top { get; set; }
		}
	}
}
