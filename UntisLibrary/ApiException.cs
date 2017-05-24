using System;
using System.Collections.Generic;
using System.Text;

namespace UntisLibrary
{
	public class ApiException : Exception
	{
		public ApiException() : base() { }
		public ApiException(string message) : base(message) { }
	}
}
