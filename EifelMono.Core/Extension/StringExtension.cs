using System;
using System.Collections.Generic;
using System.Text;

namespace ProLog2.Essentials.EifelMono.Core.Extension
{
    public static class StringExtension
    {
        public static string IfEndsWithRemoveIt(this string thisValue, string value)
        {
            thisValue = thisValue.NoNullString();
            return thisValue.EndsWith(value) ? thisValue.Substring(0, thisValue.Length - value.Length) : value;
        }
        public static string NoNullString(this string thisValue) => thisValue ?? "";
        public static bool OrContains (this string thisValue, params string[] values)
        {
            foreach (var value in values)
                if (thisValue.Contains(value))
                    return true;
            return false;
        }
        public static bool AndContains(this string thisValue, params string[] values)
        {
            foreach (var value in values)
                if (!thisValue.Contains(value))
                    return false;
            return true;
        }
    }
}
