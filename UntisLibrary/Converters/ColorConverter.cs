using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

using UntisLibrary.Models;

namespace UntisLibrary.Converters
{
	class ColorConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(Color);
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null) return null;
			if (reader.Value is string strValue)
			{
				if (strValue == "")
				{
					return null;
				}
				if (strValue[0] == '#' && strValue.Length == 7 && Byte.TryParse(strValue.Substring(1, 2), NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out var r) && Byte.TryParse(strValue.Substring(3, 2), NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out var g) && Byte.TryParse(strValue.Substring(5, 2), NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out var b))
				{
					return new Color { R = r, G = g, B = b };
				}
			}
			throw new JsonSerializationException($"Error converting \"{reader.Value}\" to Color.");
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var c = (Color)value;
			if (c == null)
			{
				writer.WriteValue("");
			}
			else
			{
				writer.WriteValue($"#{c.R:X2}{c.G:X2}{c.B:X2}");
			}
		}
	}
}
