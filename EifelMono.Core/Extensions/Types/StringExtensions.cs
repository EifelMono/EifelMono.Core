using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        #region KeyValue

        public const char KeyValueSplitChar = '=';

        public class KeyValueSplitException : Exception
        {
            public KeyValueSplitException(string message) : base(message) { }
        }

        public static (string Key, string Value) KeyValueSplit(this string item, char keyValueSplitChar = KeyValueSplitChar)
        {
            var kv = item?.Split(keyValueSplitChar);
            if (kv?.Length == 2)
                return (kv[0], kv[1]);
            throw new KeyValueSplitException($"No key, value item {item} with split='{keyValueSplitChar}' char");
        }

        #endregion

        #region Argument<T>

        public class KeyNotFoundException : Exception
        {
            public KeyNotFoundException(string message) : base(message) { }
        }

        public class ArgumentConvertValueException : Exception
        {
            public ArgumentConvertValueException(string message) : base(message) { }
        }

        public static bool HasArgument(this IEnumerable<string> items, string key)
        {
            foreach (var item in items)
            {
                try
                {
                    if (KeyValueSplit(item).Key == key)
                        return true;
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }

            }
            return false;
        }

        public static bool HasArgument<T>(this string line, string key, char splitChar = ' ')
        {
            return HasArgument(line.Split(splitChar), key);
        }

        /// <summary>
        /// Argument the specified items and key.
        /// throws an exception on error
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="items">Items.</param>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Argument<T>(this IEnumerable<string> items, string key)
        {
            foreach (var item in items)
            {
                var keyValue = KeyValueSplit(item);
                if (keyValue.Key == key)
                    try
                    {
                        if (typeof(T).IsEnum)
                            return (T)Enum.Parse(typeof(T), keyValue.Value);
                        return (T)Convert.ChangeType(keyValue.Value, typeof(T));
                    }
                    catch
                    {
                        throw new ArgumentConvertValueException($"Convert value={keyValue.Value} to type={typeof(T).Name} error, from key={keyValue.Key} in [{string.Join(" ", items)}]");
                    }
            }
            throw new KeyNotFoundException($"Key={key} not found in [{string.Join(" ", items)}]");
        }

        /// <summary>
        /// Argument the specified items, key and defaultValue.
        /// On error use default value
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="items">Items.</param>
        /// <param name="key">Key.</param>
        /// <param name="onErrorUseDefaultValue">Default value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Argument<T>(this IEnumerable<string> items, string key, T defaultValue)
        {
            try
            {
                return Argument<T>(items, key);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Argument the specified line, key and splitChar.
        /// throws an exception on error
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="line">Line.</param>
        /// <param name="key">Key.</param>
        /// <param name="splitChar">Split char.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Argument<T>(this string line, string key, char splitChar = ' ')
        {
            return Argument<T>(line.Split(splitChar), key);
        }

        /// <summary>
        /// Argument the specified line, key, onErrorUseDefaultValue and splitChar.
        /// On error use default value
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="line">Line.</param>
        /// <param name="key">Key.</param>
        /// <param name="onErrorUseDefaultValue">On error use default value.</param>
        /// <param name="splitChar">Split char.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Argument<T>(this string line, string key, T defaultValue, char splitChar = ' ')
        {
            try
            {
                return Argument<T>(line.Split(splitChar), key);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Argument the specified line, key and splitChar.
        /// throws an exception on error
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="line">Line.</param>
        /// <param name="key">Key.</param>
        /// <param name="splitChar">Split char.</param>
        public static string Argument(this string line, string key, char splitChar = ' ')
        {
            return Argument<string>(line.Split(splitChar), key);
        }

        /// <summary>
        /// Argument the specified line, key, defaultValue and splitChar.
        /// On error use default value
        /// </summary>
        /// <returns>The argument.</returns>
        /// <param name="line">Line.</param>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <param name="splitChar">Split char.</param>
        public static string Argument(this string line, string key, string defaultValue, char splitChar = ' ')
        {
            try
            {
                return Argument<string>(line.Split(splitChar), key);
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion

        #region double
        public static double ToDouble(this string value, double? defaultValue= null)
        {
            if (defaultValue!= null)
                return double.Parse(value);
            else
            {
                double result = (double)defaultValue;
                if (double.TryParse(value, out result))
                    return result;
                else
                    return (double)defaultValue;
            }
        }
        #endregion

        #region double
        public static int ToInt(this string value, int? defaultValue = null)
        {
            if (defaultValue != null)
                return int.Parse(value);
            else
            {
                int result = (int)defaultValue;
                if (int.TryParse(value, out result))
                    return result;
                else
                    return (int)defaultValue;
            }
        }
        #endregion
    }

}
