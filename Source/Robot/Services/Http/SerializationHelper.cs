using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

namespace Jonas.BitcoinPriceNotification.Robot.Services.Http
{
    internal class SerializationHelper : ISerializationHelper
    {
        public T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public string SerializeToJson(object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public string SerializeToQueryString(object data)
        {
            var stringBuilder = new StringBuilder();
            var properties = data.GetType().GetRuntimeProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name.ToLowerInvariant();
                var properyValue = property.GetValue(data);
                if (properyValue != null)
                {
                    stringBuilder.AppendFormat("{0}={1}&", propertyName, properyValue);
                }
            }
            return stringBuilder.ToString().TrimEnd('&');            
        }
    }
}