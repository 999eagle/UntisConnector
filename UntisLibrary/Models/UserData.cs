using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class UserData
	{
		// elemType null
		// elemId -1
		[JsonProperty(PropertyName = "displayName")]
		public string DisplayName { get; set; }
		[JsonProperty(PropertyName = "schoolName")]
		public string SchoolName { get; set; }
		[JsonProperty(PropertyName = "departmentId")]
		public uint DepartmentId { get; set; }
		// children []
		// rights []
	}
}
