using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using UntisLibrary.Models;

namespace UntisLibrary
{
	public sealed class API
	{
		HttpClient http;
		readonly string server;
		AuthenticateResult auth;

		static ushort requestCounter = 0;
		static string GenerateRequestID() => $"req-{(requestCounter > 999 ? (requestCounter = 0) : requestCounter++):D3}";

		public API(string server)
		{
			this.server = server;
			http = new HttpClient();
		}

		private Task<HttpResponseMessage> DoApiCall(string endpoint, string method, JToken methodParameters, string sessionID = null, IDictionary<string, string> urlParameters = null)
		{
			var requestID = GenerateRequestID();
			var requestObject = new JObject
			{
				{ "method", method },
				{ "jsonrpc", "2.0" },
				{ "id", requestID },
				{ "params", methodParameters ?? new JObject() }
			};
			var url = $"https://{server}/WebUntis/{endpoint}";
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

		private bool ProcessCookie(HttpResponseMessage response)
		{
			if (auth != null) return true;
			if (response.Headers.Contains("Set-Cookie"))
			{
				var cookies = response.Headers.GetValues("Set-Cookie");
				foreach (var cookie in cookies)
				{
					if (cookie.Contains("JESSEIONID="))
					{
						return true;
					}
				}
			}
			return false;
		}

		public async Task<bool> AuthenticateOTP(string schoolname, string user, string key)
		{
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			
			string otp = "";
			if (user != "#anonymous#")
			{
				otp = $"{Helper.CalcOTP(key, timestamp / 1000):D6}";
			}

			var response = await DoApiCall("jsonrpc_intern.do", "getUserData", new JArray
			{
				new JObject
				{
					{ "masterDataTimestamp", 0 },
					{ "auth", new JObject
					{
						{ "user", user },
						{ "otp", otp },
						{ "clientTime", $"{timestamp}" },
					} },
				}
			}, null, new Dictionary<string, string> { { "school", schoolname } });

			return ProcessCookie(response);
		}
	}
}
