using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class TimetableData
	{
		[JsonProperty(PropertyName = "lastImportTimestamp")]
		public long LastImportTimestamp { get; set; }
		[JsonProperty(PropertyName = "periods")]
		public Period[] Periods { get; set; }
		// roomLocks: []
	}
}
