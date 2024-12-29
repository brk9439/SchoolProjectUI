using Newtonsoft.Json;

namespace SchoolProjectUI.Attribute
{
    public static class Extension
    {
        public static void SetSessionObject(this ISession session, string key, object value)
        {
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }
        public static T GetSessionObject<T>(this ISession session, string key) where T : class
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }

            T value = JsonConvert.DeserializeObject<T>(objectString);
            return value;
        }
    }
}
