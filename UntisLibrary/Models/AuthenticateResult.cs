using System;
using System.Collections.Generic;
using System.Text;

namespace UntisLibrary.Models
{
	class AuthenticateResult
	{
		public string SessionID { get; set; }
		public int PersonType { get; set; }
		public int PersonID { get; set; }
		public int KlasseID { get; set; }
	}
}
