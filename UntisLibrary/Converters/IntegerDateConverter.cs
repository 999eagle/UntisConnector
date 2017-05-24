using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UntisLibrary.Converters
{
	class IntegerDateConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (!(reader.Value is long longValue))
			{
				throw new JsonSerializationException($"Error converting \"{reader.Value}\" to DateTime.");
			}
			int value = (int)longValue;
			int day = value % 100;
			value = (value - day) / 100;
			int month = value % 100;
			int year = (value - month) / 100;
			return new DateTime(year, month, day);
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dt = (DateTime)value;
			writer.WriteValue(dt.Year * 10000 + dt.Month * 100 + dt.Day);
		}
	}
}
