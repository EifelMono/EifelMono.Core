using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;

namespace EifelMono.Core.Extensions
{
    public static class GenericExtensions
    {
        #region In

        public static bool In<T>(this T value, IEnumerable<T> choices)
        {
            foreach (var choice in choices)
                if (choice.Equals(value))
                    return true;
            return false;
        }

        public static bool In<T>(this T value, params T[] choices)
        {
            return In(value, choices as IEnumerable<T>);
        }

        public static bool In<T>(this IEnumerable<T> values, IEnumerable<T> choices)
        {
            foreach (var value in values)
                if (value.In(choices))
                    return true;
            return false;
        }

        public static bool In<T>(this IEnumerable<T> values, params T[] choices)
        {
            return In(values, choices as IEnumerable<T>);
        }

        #endregion

        #region Out

        public static bool Out<T>(this T value, IEnumerable<T> choices)
        {
            return !In(value, choices);
        }

        public static bool Out<T>(this T value, params T[] choices)
        {
            return !In(value, choices);
        }

        public static bool Out<T>(this IEnumerable<T> values, IEnumerable<T> choices)
        {
            return !In(values, choices);
        }

        public static bool Out<T>(this IEnumerable<T> values, params T[] choices)
        {
            return !In(values, choices);
        }

        #endregion

        #region InRange

        public static bool InRange<T>(this T value, T minChoice, T maxChoise) where T : IComparable
        {
            return value.CompareTo(minChoice) >= 0 && value.CompareTo(maxChoise) <= 0;
        }

        public static bool InRange<T>(this IEnumerable<T> values, T minChoice, T maxChoise) where T : IComparable
        {
            bool result = false;
            foreach (var value in values)
            {
                result = value.InRange(minChoice, maxChoise);
                if (result)
                    return result;
            }
            return result;
        }

        public static T ConvertInRange<T>(this T value, T minChoice, T maxChoise) where T : IComparable
        {
            if (value.CompareTo(minChoice) < 0)
                return minChoice;
            if (value.CompareTo(maxChoise) > 0)
                return maxChoise;
            return value;
        }

        #endregion

        #region OutRange

        public static bool OutRange<T>(this T value, T minChoice, T maxChoise) where T : IComparable
        {
            return !InRange(value, minChoice, maxChoise);
        }

        public static bool OutRange<T>(this IEnumerable<T> values, T minChoice, T maxChoise) where T : IComparable
        {
            return !InRange(values, minChoice, maxChoise);
        }

        #endregion

        #region PositionOf

        public static int PositionOf<T>(this T value, IEnumerable<T> compareValues)
        {
            int index = -1;
            foreach (var compareValue in compareValues)
            {
                index++;
                if (compareValue.Equals(value))
                    return index;
            }
            return -1;
        }

        public static int PositionOf<T>(this T value, params T[] compareValues)
        {
            return PositionOf(value, compareValues as IEnumerable<T>);
        }

        public class PositionOfResult
        {
            public static PositionOfResult New()
            {
                return new PositionOfResult();
            }

            public PositionOfResult ClearResult()
            {
                Result1 = -1;
                Result2 = -1;
                return this;
            }

            public int Result1 { get; set; } = -1;

            public int Result2 { get; set; } = -1;

            public int Result { get { return Result1 + Result2; } }
        }

        public static PositionOfResult PositionOf<T>(this IEnumerable<T> values, IEnumerable<T> compareValues)
        {
            PositionOfResult result = PositionOfResult.New();
            int Result1 = -1;
            foreach (var value in values)
            {
                Result1++;
                var Result2 = value.PositionOf(compareValues);
                if (Result2 >= 0)
                    return result;
            }
            return result.ClearResult();
        }

        public static PositionOfResult PositionOf<T>(this IEnumerable<T> values, params T[] compareValues)
        {
            return PositionOf(values, compareValues as IEnumerable<T>);
        }

        #endregion

        public static IEnumerable<T> Inverse<T>(this IList<T> list)
        {
            int count = list.Count;
            for (int i = count - 1; i >= 0; --i)
                yield return list[i];
        }

        public static IEnumerable<T> Inverse<T>(this T[] array)
        {
            int length = array.Length;
            for (int i = length - 1; i >= 0; --i)
                yield return array[i];
        }

        public static IEnumerable<T> Inverse<T>(this Array array)
        {
            int length = array.Length;
            for (int i = length - 1; i >= 0; --i)
                yield return (T)array.GetValue(i);
        }

        public static IEnumerable<T> Inverse<T>(this IEnumerable<T> elements)
        {
            var list = elements.ToList();
            return list.Inverse();
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        #region Pipe

        public static T Pipe<T>(this T pipe, Action<T> action)
        {
            action(pipe);
            return pipe;
        }

        public static T Pipe<T>(this T pipe, Func<T, T> action)
        {
            return action(pipe);
        }

        public static T Clone<T>(this T value)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value));
        }
        #endregion


    }
}
