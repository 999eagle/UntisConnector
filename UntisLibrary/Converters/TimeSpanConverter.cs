using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Converters
{
	class TimeSpanConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = (string)reader.Value;
			if (value[0] == 'T')
			{
				value = value.Substring(1);
				var idx = value.IndexOf(':');
				if (idx == 2 && Int32.TryParse(value.Substring(0, idx), out int h) && Int32.TryParse(value.Substring(idx + 1), out int m))
				{
					return new TimeSpan(h, m, 0);
				}
			}
			throw new JsonSerializationException($"Error converting \"{reader.Value}\" to TimeSpan.");
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ts = (TimeSpan)value;
			writer.WriteValue($"T{ts.Hours:D2}:{ts.Minutes:D2}");
		}
	}
}
