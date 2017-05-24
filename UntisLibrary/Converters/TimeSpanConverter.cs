using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Converters
{
	class TimeSpanConverter : JsonConverter
	{
		private bool serializeAsInt;
		public TimeSpanConverter() : this(false) { }
		public TimeSpanConverter(bool serializeAsInt)
		{
			this.serializeAsInt = serializeAsInt;
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(TimeSpan);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value is long longValue)
			{
				if (longValue == 0)
				{
					return new TimeSpan(0, 0, 0);
				}
				var intVal = (int)longValue;
				int m = intVal % 100;
				int h = (intVal - m) / 100;
				return new TimeSpan(h, m, 0);
			}
			else if (reader.Value is string value)
			{
				if (value[0] == 'T')
				{
					value = value.Substring(1);
					var idx = value.IndexOf(':');
					if (idx == 2 && Int32.TryParse(value.Substring(0, idx), out int h) && Int32.TryParse(value.Substring(idx + 1), out int m))
					{
						return new TimeSpan(h, m, 0);
					}
				}
			}
			throw new JsonSerializationException($"Error converting \"{reader.Value}\" to TimeSpan.");
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ts = (TimeSpan)value;
			if (serializeAsInt)
			{
				writer.WriteValue(ts.Hours * 100 + ts.Minutes);
			}
			else
			{
				writer.WriteValue($"T{ts.Hours:D2}:{ts.Minutes:D2}");
			}
		}
	}
}
