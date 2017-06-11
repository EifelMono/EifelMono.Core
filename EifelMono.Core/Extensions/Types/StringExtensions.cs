using System;
using System.Collections.Generic;
using System.Linq;

namespace EifelMono.Core.Extensions
{
    public static class StringExtensions
    {
        #region ...

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNull(this string value)
        {
            return value == null;
        }

        public static bool IsEmpty(this string value)
        {
            if (value != null)
                return value == "";
            else
                return false;
        }

        #endregion

        #region InContains

        public static bool InContains(this string value, IEnumerable<string> choices)
        {
            foreach (var choice in choices)
                if (value.Contains(choice))
                    return true;
            return false;
        }

        public static bool InContains(this string value, params string[] choices)
        {
            return InContains(value, choices as IEnumerable<string>);
        }

        public static bool InContains(this IEnumerable<string> values, IEnumerable<string> choices)
        {
            foreach (var value in values)
                if (value.InContains(choices))
                    return true;
            return false;
        }

        public static bool InContains(this IEnumerable<string> values, params string[] choices)
        {
            return InContains(values, choices as IEnumerable<string>);
        }

        #endregion

        #region InStartsWith

        public static bool InStartsWith(this string value, IEnumerable<string> choices)
        {
            foreach (string choice in choices)
                if (value.StartsWith(choice))
                    return true;
            return false;
        }

        public static bool InStartsWith(this string value, params string[] choices)
        {
            return InStartsWith(value, choices as IEnumerable<string>);
        }

        public static bool InStartsWith(this IEnumerable<string> values, IEnumerable<string> choices)
        {
            foreach (var value in values)
                if (value.InStartsWith(choices))
                    return true;
            return false;
        }

        public static bool InStartsWith(this IEnumerable<string> values, params string[] choices)
        {
            return InStartsWith(values, choices as IEnumerable<string>);
        }

        #endregion

        #region InEndsWith

        public static bool InEndsWith(this string value, IEnumerable<string> choices)
        {
            foreach (var choice in choices)
                if (value.EndsWith(choice, StringComparison.Ordinal))
                    return true;
            return false;
        }

        public static bool InEndsWith(this string value, params string[] choices)
        {
            return InEndsWith(value, choices as IEnumerable<string>);
        }

        public static bool InEndsWith(this IEnumerable<string> values, IEnumerable<string> choices)
        {
            foreach (var value in values)
                if (value.InEndsWith(choices))
                    return true;
            return false;
        }

        public static bool InEndsWith(this IEnumerable<string> values, params string[] choices)
        {
            return InEndsWith(values, choices as IEnumerable<string>);
        }

        #endregion

        #region InLength

        public static bool InLength(this string value, IEnumerable<int> choices)
        {
            foreach (var choice in choices)
                if (value.Length == choice)
                    return true;
            return false;
        }

        public static bool InLength(this string value, params int[] choices)
        {
            return InLength(value, choices as IEnumerable<int>);
        }

        public static bool InLength(this IEnumerable<string> values, IEnumerable<int> choices)
        {
            foreach (var value in values)
                if (value.InLength(choices))
                    return true;
            return false;
        }

        public static bool InLength(this IEnumerable<string> values, params int[] choices)
        {
            return InLength(values, choices as IEnumerable<int>);
        }

        #endregion

        #region Dot...

        public static string DotPart(this string value, bool dir = true, int index = 0, int range = 1)
        {
            if (string.IsNullOrEmpty(value))
                return "";
            var items = value.Split('.');

            string result = "";
            int pos = dir ? 0 + index : items.Length - index - range;
            for (int i = index; i < index + range; i++)
            {
                if (pos.InRange(0, items.Length - 1))
                    result += (result.Length == 0 ? "" : ".") + items[pos];
                pos++;
            }
            return result;
        }

        public static string DotFirst(this string value, int range = 1)
        {
            return value.DotPart(true, 0, range);
        }

        public static string DotLast(this string value, int range = 1)
        {
            return value.DotPart(false, 0, range);
        }

        #endregion

        #region SubString

        public static string Before(this string value, string search)
        {
            int pos = value.IndexOf(search, StringComparison.Ordinal);
            return pos != -1 ? value.Substring(0, pos) : "";
        }

        public static string LastBefore(this string value, string search)
        {
            int pos = value.LastIndexOf(search, StringComparison.Ordinal);
            return pos != -1 ? value.Substring(0, pos) : "";
        }

        public static string After(this string value, string search)
        {
            int pos = value.IndexOf(search, StringComparison.Ordinal);
            return pos != -1 ? value.Substring(pos + search.Length) : "";
        }

        public static string LastAfter(this string value, string search)
        {
            int pos = value.LastIndexOf(search, StringComparison.Ordinal);
            return pos != -1 ? value.Substring(pos + search.Length) : "";
        }

        #endregion

        #region Url

        public static string UrlCombine(this string url, params string[] paths)
        {
            return url.TrimEnd('/') + '/' + paths.Aggregate(
                (furl, path) => string.Format("{0}/{1}", furl.TrimEnd('/'), path.TrimStart('/').TrimEnd('/'))).TrimStart('/').TrimEnd('/');
        }

        #endregion

        #region KeyValue KVPair

        public static string[] KVPairToArray(this string value, char split = '=')
        {
            if (value.IsNullOrEmpty())
                return null;
            return value.Split(split);
        }

        public static string KeyFromKVPair(this string value)
        {
            var kvArray = value.KVPairToArray();
            if (kvArray == null)
                return "";
            if (kvArray.Length != 2)
                return "";
            return kvArray[0];
        }

        public static string ValueFromKVPair(this string value)
        {
            var kvArray = value.KVPairToArray();
            if (kvArray == null)
                return "";
            if (kvArray.Length != 2)
                return "";
            return kvArray[1];
        }

        #endregion
    }

}
