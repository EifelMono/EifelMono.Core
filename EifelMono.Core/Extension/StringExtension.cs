using System;
using System.Collections.Generic;

namespace EifelMono.Core.Extension
{
    public static class StringExtension
    {
        public static string FixNullString(this string thisValue)
            => thisValue ?? "";

        public static bool IsNullOrEmpty(this string thisValue)
            => string.IsNullOrEmpty(thisValue);

        public static bool IsEmpty(this string thisValue)
            => thisValue != null ? thisValue == "" : false;
  
        public static string IfEndsWithRemoveIt(this string thisValue, string value)
        {
            thisValue = thisValue.FixNullString();
            return thisValue.EndsWith(value) ? thisValue.Substring(0, thisValue.Length - value.Length) : value;
        }

        public static bool InContainsOne(this string thisValue, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (thisValue.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool InContainsOne(this string thisValue, params string[] values)
            => thisValue.InContainsOne(values as IEnumerable<string>);


        public static bool InContainsAll(this string thisValue, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (!thisValue.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool InContainsAll(this string thisValue, string[] values)
            => thisValue.InContainsAll(values as IEnumerable<string>);

        public static bool InStartsWith(this string thisValue, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (thisValue.StartsWith(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool InStartsWith(this string thisValue, params string[] values)
            => thisValue.InStartsWith(values as IEnumerable<string>);

        public static bool InEndsWith(this string thisValue, IEnumerable<string> values)
        {
            foreach (string choice in values)
            {
                if (thisValue.EndsWith(choice, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool InEndsWith(this string thisValue, params string[] values)
        => thisValue.InEndsWith(values as IEnumerable<string>);

        public static bool InLength(this string thisValue, IEnumerable<int> values)
        {
            foreach (int value in values)
            {
                if (thisValue.Length == value)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool InLength(this string thisValue, params int[] values)
            => thisValue.InLength(values as IEnumerable<int>);


        public static string Before(this string thisValue, string value)
        {
            int pos = thisValue.IndexOf(value, StringComparison.Ordinal);
            return pos != -1 ? thisValue.Substring(0, pos) : "";
        }

        public static string LastBefore(this string thisValue, string value)
        {
            int pos = thisValue.LastIndexOf(value, StringComparison.Ordinal);
            return pos != -1 ? thisValue.Substring(0, pos) : "";
        }

        public static string After(this string thisValue, string value)
        {
            int pos = thisValue.IndexOf(value, StringComparison.Ordinal);
            return pos != -1 ? thisValue.Substring(pos + value.Length) : "";
        }

        public static string LastAfter(this string thisValue, string value)
        {
            int pos = thisValue.LastIndexOf(value, StringComparison.Ordinal);
            return pos != -1 ? thisValue.Substring(pos + value.Length) : "";
        }


        #region Converting
        /// <summary>
        /// ToDouble ,if no defaultValue 
        /// a Excption could be happen
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string value, double? defaultValue = null)
        {
            if (defaultValue != null)
            {
                return double.Parse(value);
            }
            else
            {
                double result = (double)defaultValue;
                if (double.TryParse(value, out result))
                {
                    return result;
                }
                else
                {
                    return (double)defaultValue;
                }
            }
        }

        public static int ToInt(this string value, int? defaultValue = null)
        {
            if (defaultValue != null)
            {
                return int.Parse(value);
            }
            else
            {
                int result = (int)defaultValue;
                if (int.TryParse(value, out result))
                {
                    return result;
                }
                else
                {
                    return (int)defaultValue;
                }
            }
        }

        #endregion
    }
}
