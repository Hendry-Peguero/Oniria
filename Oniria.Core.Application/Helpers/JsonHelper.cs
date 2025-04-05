using Newtonsoft.Json;

namespace Oniria.Core.Application.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T value)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(value, settings);
        }

        public static T? Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json ?? string.Empty);
        }
    }
}
