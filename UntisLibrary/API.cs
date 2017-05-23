using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace UntisLibrary
{
	public sealed class API
	{
		HttpClient http;
		readonly string server;

		static ushort requestCounter = 0;
		static string GenerateRequestID() => $"req-{(requestCounter > 999 ? (requestCounter = 0) : requestCounter++):D3}";

		public API(string server)
		{
			this.server = server;
			http = new HttpClient();
		}

		private Task<HttpResponseMessage> DoApiCall(string url, string method, JObject methodParameters, string sessionID = null, IDictionary<string, string> urlParameters = null)
		{
			var requestID = GenerateRequestID();
			var requestObject = new JObject
			{
				{ "method", method },
				{ "jsonrpc", "2.0" },
				{ "id", requestID },
				{ "params", methodParameters ?? new JObject() }
			};
			if (urlParameters != null)
			{
				url += "?" + urlParameters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}").Aggregate((a, b) => $"{a}&{b}");
			}
			var reqMessage = new HttpRequestMessage(HttpMethod.Post, url)
			{
				Content = new StringContent(requestObject.ToString(Newtonsoft.Json.Formatting.None), Encoding.UTF8, "application/json")
			};
			if (sessionID != null)
			{
				reqMessage.Headers.Add("Cookie", $"JSESSIONID={sessionID}");
			}
			return http.SendAsync(reqMessage);
		}
	}
}
