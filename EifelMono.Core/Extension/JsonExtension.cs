using EifelMono.Core.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.Core.Extension
{
    public static class JsonExtension
    {
        public static T JsonClone<T>(this T thisValue, T defaultValue = default(T))
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(thisValue));
            }
            catch (Exception ex)
            {
                ex.LogException();
                return defaultValue;
            }
        }

        public static string ToJsonString(this object thisValue)
        {
            try
            {
                return JsonConvert.SerializeObject(thisValue, Formatting.Indented);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return ex.ToString();
            }
        }
    }
}
