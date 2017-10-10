using Infrastructure.Repositories;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Extensions
{
    internal static class JsonExtensions
    {
        public static JProperty ToJProperty(this StoreKey src, JProperty[] properties)
        {
            var jObject = new JObject();
            foreach (var jProperty in properties)
            {
                jObject.Add(jProperty);
            }
            return new JProperty(src.ToString(), jObject);
        }

        public static JProperty ToJProperty(this StoreKey src, object content)
            => new JProperty(src.ToString(), content);
    }
}