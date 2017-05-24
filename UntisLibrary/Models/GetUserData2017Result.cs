using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class GetUserData2017Result
	{
		[JsonProperty(PropertyName = "masterData")]
		public MasterData MasterData { get; set; }
		[JsonProperty(PropertyName = "userData")]
		public UserData UserData { get; set; }
	}
}
