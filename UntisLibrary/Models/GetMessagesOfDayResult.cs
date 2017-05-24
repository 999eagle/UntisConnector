using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class GetMessagesOfDayResult
	{
		[JsonProperty(PropertyName = "messageOfDayCollection")]
		public MessageOfDayCollection Messages { get; set; }
	}
}
