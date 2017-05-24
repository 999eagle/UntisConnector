using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class MessageOfDay
	{
		[JsonProperty(PropertyName = "subject")]
		public string Subject { get; set; }
		[JsonProperty(PropertyName = "text")]
		public string HtmlText { get; set; }
	}
}
