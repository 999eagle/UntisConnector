using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace UntisLibrary
{
	public class JsonRpcException : Exception
	{
		public JsonRpcException() : base() { }
		public JsonRpcException(string message) : base(message) { }
	}

	public class ApiException : Exception
	{
		public int ErrorCode { get; private set; }
		public string URL { get; private set; }
		public JObject Request { get; private set; }
		public ApiException(string message, int errorCode, string url, JObject request) : base($"API returned error: \"{message}\".")
		{
			ErrorCode = errorCode;
			URL = url;
			Request = request;
		}
	}
}
