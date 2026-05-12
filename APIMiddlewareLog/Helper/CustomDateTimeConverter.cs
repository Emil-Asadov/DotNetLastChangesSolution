using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Globalization;

namespace APIMiddlewareLog.Helper
{
    public class CustomDateTimeConverter(string dateFormat) : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?) || objectType == typeof(DateTime);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            return DateTime.ParseExact((string)reader.Value!, dateFormat, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((DateTime)value).ToString(dateFormat, CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
