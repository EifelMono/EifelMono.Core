using EifelMono.Core.Extension;
using EifelMono.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EifelMono.Core.Extensions
{
    public static class StringExtensions
    {
    #region Dot...

    public static string DotPart(this string thisValue, bool dir = true, int index = 0, int range = 1)
    {
        if (string.IsNullOrEmpty(thisValue))
        {
            return "";
        }

        string[] items = thisValue.Split('.');

        string result = "";
        int pos = dir ? 0 + index : items.Length - index - range;
        for (int i = index; i < index + range; i++)
        {
            if (pos.InRangeOffset(0, items.Length - 1))
            {
                result += (result.Length == 0 ? "" : ".") + items[pos];
            }

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

  
    #endregion


    #region KeyValue

    public const char KeyValueSplitChar = '=';

    public class KeyValueSplitException : Exception
    {
        public KeyValueSplitException(string message) : base(message) { }
    }

    public static (string Key, string Value) KeyValueSplit(this string item, char keyValueSplitChar = KeyValueSplitChar)
    {
        string[] kv = item?.Split(keyValueSplitChar);
        if (kv?.Length == 2)
        {
            return (kv[0], kv[1]);
        }

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
        foreach (string item in items)
        {
            try
            {
                if (KeyValueSplit(item).Key == key)
                {
                    return true;
                }
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
        foreach (string item in items)
        {
            var keyValue = KeyValueSplit(item);
            if (keyValue.Key == key)
            {
                try
                {
                    if (typeof(T).IsEnum)
                    {
                        return (T)Enum.Parse(typeof(T), keyValue.Value);
                    }

                    return (T)Convert.ChangeType(keyValue.Value, typeof(T));
                }
                catch
                {
                    throw new ArgumentConvertValueException($"Convert value={keyValue.Value} to type={typeof(T).Name} error, from key={keyValue.Key} in [{string.Join(" ", items)}]");
                }
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


}

}
