using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Bookstore.Models
{
    // same as in chapter - make it easier to get and set objects in session

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value) => 
            session.SetString(key, JsonConvert.SerializeObject(value));

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
