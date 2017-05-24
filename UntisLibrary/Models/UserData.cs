using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Models
{
	public class UserData
	{
		[JsonProperty(PropertyName = "elemType")]
		public ElementType? ElementType { get; set; }
		[JsonProperty(PropertyName = "elemId")]
		public int ElementId { get; set; }
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
