using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Converters
{
	class DayOfWeekConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(DayOfWeek);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.Value)
			{
				case "MON": return DayOfWeek.Monday;
				case "TUE": return DayOfWeek.Tuesday;
				case "WED": return DayOfWeek.Wednesday;
				case "THU": return DayOfWeek.Thursday;
				case "FRI": return DayOfWeek.Friday;
				case "SAT": return DayOfWeek.Saturday;
				case "SUN": return DayOfWeek.Sunday;
			}
			return null;
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			switch ((DayOfWeek)value)
			{
				case DayOfWeek.Monday:
					writer.WriteValue("MON");
					break;
				case DayOfWeek.Tuesday:
					writer.WriteValue("TUE");
					break;
				case DayOfWeek.Wednesday:
					writer.WriteValue("WED");
					break;
				case DayOfWeek.Thursday:
					writer.WriteValue("THU");
					break;
				case DayOfWeek.Friday:
					writer.WriteValue("FRI");
					break;
				case DayOfWeek.Saturday:
					writer.WriteValue("SAT");
					break;
				case DayOfWeek.Sunday:
					writer.WriteValue("SUN");
					break;
			}
		}
	}
}
