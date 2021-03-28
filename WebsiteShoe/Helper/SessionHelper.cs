using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Helper
{
    public static class SessionHelper 
    {
        public static void SetObjectAsJson(ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(ISession session,string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
