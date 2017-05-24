using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	[JsonObject]
	public class MessageOfDayCollection : IEnumerable<MessageOfDay>
	{
		[JsonProperty(PropertyName = "messages")]
		public MessageOfDay[] Messages { get; set; }

		IEnumerator IEnumerable.GetEnumerator() => Messages.GetEnumerator();
		IEnumerator<MessageOfDay> IEnumerable<MessageOfDay>.GetEnumerator() => ((IEnumerable<MessageOfDay>)Messages).GetEnumerator();
	}
}
