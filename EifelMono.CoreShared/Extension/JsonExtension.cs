using Newtonsoft.Json;

namespace EifelMono.Core.Extension
{
    public static class JsonExtension
    {
        public static T JsonClone<T>(this T thisValue)
            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(thisValue));

        public static string ToJsonString(this object thisValue, Formatting formating = Formatting.Indented)
            => JsonConvert.SerializeObject(thisValue, formating);
    }
}
