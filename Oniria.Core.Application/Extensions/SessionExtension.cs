using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Oniria.Core.Application.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var jsonData = JsonConvert.SerializeObject(value, settings);
            session.SetString(key, jsonData);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
