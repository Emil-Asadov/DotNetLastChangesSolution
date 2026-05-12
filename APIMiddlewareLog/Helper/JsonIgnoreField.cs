using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIMiddlewareLog.Helper
{
    public class JsonIgnoreField
    {
        public static void RemoveEmptyObjectProperties(JObject jobj)
        {
            var propertiesWithObjectValues =
                jobj
                .OfType<JProperty>()
                .Where(p => p.Value is JObject);

            //    
            // Since we are going to manipulate the JObject while we are iterating the properties of said JObject,
            // it is probably safer to iterate over a copy of the property list instead of directly iterating over
            //  the JObject while it is being manipulated.
            //    
            propertiesWithObjectValues = propertiesWithObjectValues.ToList();

            foreach (var jprop in propertiesWithObjectValues)
            {
                var propValue = (JObject)jprop.Value;
                RemoveEmptyObjectProperties(propValue);
                if (propValue.Count == 0)
                    jobj.Remove(jprop.Name);
            }
        }

        public static string GetJson<T>(T model)
        {
            var json = string.Empty;
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    //DefaultValueHandling = DefaultValueHandling.Ignore, //Type-n Default qiymetine gore Field-i goturmur
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    Converters = new List<JsonConverter> { new CustomDateTimeConverter("yyyy-MM-dd") }
                };
                var serializer = JsonSerializer.Create(serializerSettings);

                var nullableJson = JObject.FromObject(model, serializer);
                RemoveEmptyObjectProperties(nullableJson);

                json = nullableJson.ToString();

                return json;
            }
            catch
            {
                throw;
            }
        }
    }
}
