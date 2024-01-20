using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fintranet.TaxCalculation.Infrastructure.Helpers
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object data)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public static string ToJson(this object data, IContractResolver contractResolver)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public static T Cast<T>(this object value)
        {
            return (T)value;
        }
    }
}