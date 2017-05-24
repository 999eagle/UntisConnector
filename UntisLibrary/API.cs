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
		string username = null;
		string key = null;

		public MasterData MasterData { get; private set; }
		public long MasterDataTimestamp { get => MasterData?.TimeStamp ?? 0; }

		public bool IsAuthenticated { get => username != null && key != null; }

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

		/// <summary>
		/// Sets username and password for authentication. If the specified credentials are invalid, an <see cref="ApiException"/> is thrown.
		/// </summary>
		/// <param name="username">The username to authenticate as.</param>
		/// <param name="password">The password to authenticate with.</param>
		public async Task SetAuthenticationData(string username, string password)
		{
			var key = await GetAppSharedSecret(username, password);
			this.username = username;
			this.key = key;
		}

		public async Task SetAnonymousAuthentication()
		{
			await SetAuthenticationData("#anonymous#", "");
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

		private JObject GetAuthenticationObject()
		{
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			string otp = $"{Helper.CalcOTP(key, timestamp / 1000):D6}";
			return new JObject
			{
				{ "user", username },
				{ "otp", otp },
				{ "clientTime", $"{timestamp}" },
			};
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
			if (obj["jsonrpc"].Value<string>() != JsonRpcVersion)
			{
				throw new JsonRpcException($"Invalid JsonRPC version returned by server (got: \"{obj["jsonrpc"]}\" expected: \"{JsonRpcVersion}\").");
			}
			if (obj["id"].Value<string>() != requestID)
			{
				throw new JsonRpcException($"Invalid response ID returned by server (got: \"{obj["id"]}\" expected: \"{requestID}\").");
			}
			if (obj["error"] is JObject error)
			{
				throw new ApiException(error.Value<string>("message"), error.Value<int>("code"), url, requestObject);
			}
			return obj["result"];
		}

		private T[] ArrayMergeHelper<T>(T[] existingData, T[] newData, Func<T, T, bool> equalityPredicate, Func<T, T, T> mergeElement = null)
		{
			if (newData.Any())
			{
				var list = existingData.ToList();
				foreach (var newElement in newData)
				{
					var existing = list.Where((e => equalityPredicate(e, newElement))).FirstOrDefault();
					if (existing != null)
					{
						var idx = list.IndexOf(existing);
						if (mergeElement == null)
						{
							list[idx] = newElement;
						}
						else
						{
							list[idx] = mergeElement(existing, newElement);
						}
					}
					else
					{
						list.Add(newElement);
					}
				}
				return list.ToArray();
			}
			return existingData;
		}

		private void MergeMasterData(MasterData newData)
		{
			if (MasterData == null)
			{
				MasterData = newData;
				return;
			}
			if (newData.TimeStamp < MasterData.TimeStamp) return;

			MasterData.Classes = ArrayMergeHelper(MasterData.Classes, newData.Classes, ((c1, c2) => c1.ID == c2.ID));
			MasterData.Departments = ArrayMergeHelper(MasterData.Departments, newData.Departments, ((c1, c2) => c1.ID == c2.ID));
			MasterData.Holidays = ArrayMergeHelper(MasterData.Holidays, newData.Holidays, ((c1, c2) => c1.ID == c2.ID));
			MasterData.Rooms = ArrayMergeHelper(MasterData.Rooms, newData.Rooms, ((c1, c2) => c1.ID == c2.ID));
			MasterData.Subjects = ArrayMergeHelper(MasterData.Subjects, newData.Subjects, ((c1, c2) => c1.ID == c2.ID));
			MasterData.Teachers = ArrayMergeHelper(MasterData.Teachers, newData.Teachers, ((c1, c2) => c1.ID == c2.ID));

			MasterData.TimeGrid.Days = ArrayMergeHelper(MasterData.TimeGrid.Days, newData.TimeGrid.Days, ((d1, d2) => d1.Day == d2.Day), MergeTimeGridDays);
			MasterData.TimeStamp = newData.TimeStamp;

			TimeGridDay MergeTimeGridDays(TimeGridDay existing, TimeGridDay newDay)
			{
				existing.Units = ArrayMergeHelper(existing.Units, newDay.Units, (u1, u2) => u1.StartTime == u2.StartTime && u1.EndTime == u2.EndTime);
				return existing;
			}
		}

		public async Task<UserData> GetUserData2017()
		{
			var response = await DoApiCall("jsonrpc_intern.do", "getUserData2017", new JArray
			{
				new JObject
				{
					{ "masterDataTimestamp", MasterDataTimestamp },
					{ "auth", GetAuthenticationObject() },
				}
			});
			var responseData = response.ToObject<GetUserData2017Result>();
			MergeMasterData(responseData.MasterData);
			return responseData.UserData;
		}

		public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(Class cls, DateTime startDate, uint numOfDays) => GetTimetable2017(cls.ID, DataObjectType.Class, startDate, numOfDays);
		public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(Room room, DateTime startDate, uint numOfDays) => GetTimetable2017(room.ID, DataObjectType.Room, startDate, numOfDays);
		//public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(Student student, DateTime startDate, uint numOfDays) => GetTimetable2017(student.ID, DataObjectType.Student, startDate, numOfDays);
		public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(Subject subject, DateTime startDate, uint numOfDays) => GetTimetable2017(subject.ID, DataObjectType.Subject, startDate, numOfDays);
		public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(Teacher teacher, DateTime startDate, uint numOfDays) => GetTimetable2017(teacher.ID, DataObjectType.Teacher, startDate, numOfDays);
		public Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(uint id, DataObjectType type, DateTime startDate, uint numOfDays) => GetTimetable2017(id, (uint)type, startDate, numOfDays);
		public async Task<(TimetableData timetableData, TimetableFormat timetableFormat)> GetTimetable2017(uint id, uint type, DateTime startDate, uint numOfDays)
		{
			var response = await DoApiCall("jsonrpc_mobile.do", "getTimetable2017", new JArray
			{
				new JObject
				{
					{ "masterDataTimestamp", MasterDataTimestamp },
					{ "auth", GetAuthenticationObject() },
					{ "id", id },
					{ "type", type },
					{ "startDate", startDate.ToString("yyyy-MM-dd") },
					{ "numOfDays", numOfDays }
				}
			});
			var responseData = response.ToObject<GetTimetable2017Result>();
			MergeMasterData(responseData.MasterData);
			return (responseData.TimetableData, responseData.TimetableFormat);
		}

		public Task<MessageOfDayCollection> GetMessagesOfDay() => GetMessagesOfDay(DateTime.Today);
		public async Task<MessageOfDayCollection> GetMessagesOfDay(DateTime date)
		{
			var response = await DoApiCall("jsonrpc_intern.do", "getMessagesOfDay", new JArray
			{
				new JObject
				{
					{ "auth", GetAuthenticationObject() },
					{ "date", date.Year * 10000 + date.Month * 100 + date.Day }
				}
			});
			var responseData = response.ToObject<GetMessagesOfDayResult>();
			return responseData.Messages;
		}
	}
}
