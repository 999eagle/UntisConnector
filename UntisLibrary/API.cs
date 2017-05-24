using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using UntisLibrary.Models;

namespace UntisLibrary
{
	public sealed class API
	{
		HttpClient http;
		readonly string server;
		readonly string schoolname;

		static ushort requestCounter = 0;
		static string GenerateRequestID() => $"req-{(requestCounter > 999 ? (requestCounter = 0) : requestCounter++):D3}";

		const string JsonRpcVersion = "2.0";

		public API(string server, string schoolname)
		{
			this.server = server;
			this.schoolname = schoolname;

			var cookies = new CookieContainer();
			var handler = new HttpClientHandler()
			{
				CookieContainer = cookies
			};
			http = new HttpClient(handler);
		}

		private async Task<JToken> DoApiCall(string endpoint, string method, JToken methodParameters, IDictionary<string, string> urlParameters = null)
		{
			var requestID = GenerateRequestID();
			var requestObject = new JObject
			{
				{ "method", method },
				{ "jsonrpc", JsonRpcVersion },
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

			var response = await http.SendAsync(reqMessage);

			var obj = JObject.Parse(await response.Content.ReadAsStringAsync());
			if (obj["jsonrpc"].Value<string>() != JsonRpcVersion) { throw new ApiException($"Invalid JsonRPC version returned by server (got: \"{obj["jsonrpc"]}\" expected: \"{JsonRpcVersion}\")."); }
			if (obj["id"].Value<string>() != requestID) { throw new ApiException($"Invalid response ID returned by server (got: \"{obj["id"]}\" expected: \"{requestID}\")."); }
			return obj["result"];
		}

		public async Task<bool> AuthenticateOTP(string user, string key)
		{
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			
			string otp = $"{Helper.CalcOTP(key, timestamp / 1000):D6}";

			var response = await DoApiCall("jsonrpc_intern.do", "getUserData2017", new JArray
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
			}, new Dictionary<string, string> { { "school", schoolname } });
			var responseData = response.ToObject<GetUserData2017Result>();

			return true;
		}

		public async Task<bool> AuthenticateAnonymous()
		{
			var secret = await GetAppSharedSecret("#anonymous#", "");
			return await AuthenticateOTP("#anonymous#", secret);
		}

		private async Task<string> GetAppSharedSecret(string username, string password)
		{
			var response = await DoApiCall("jsonrpc_intern.do", "getAppSharedSecret", new JArray
			{
				new JObject
				{
					{ "userName", username },
					{ "password", password },
				}
			}, new Dictionary<string, string> { { "school", schoolname } });

			return response.Value<string>();
		}
	}
}
