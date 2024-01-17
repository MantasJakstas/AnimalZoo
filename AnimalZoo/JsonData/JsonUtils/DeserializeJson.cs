using Newtonsoft.Json;

namespace AnimalZoo.JsonData.JsonUtils
{
    public static class DeserializeJson
    {
        public static T DeserializeRootJson<T>(string path)
        {
            using StreamReader reader = new(path);
            var json = reader.ReadToEnd();
            T data = JsonConvert.DeserializeObject<T>(json) ?? throw new ArgumentNullException();
            if (data == null)
            {
                throw new ArgumentNullException($"Failed to deserialize JSON into {typeof(T)} object.");
            }
            return data;
        }
    }
}
