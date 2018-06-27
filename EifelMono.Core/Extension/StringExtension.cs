using System;
using System.Collections.Generic;
using System.Text;

namespace ProLog2.Essentials.EifelMono.Core.Extension
{
    public static class StringExtension
    {
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
