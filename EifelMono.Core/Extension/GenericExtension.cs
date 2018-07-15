using System.Collections.Generic;

namespace EifelMono.Core.Extension
{
    public static class GenericExtension
    {
        public static bool IsNull<T>(this T thisValue)
            => thisValue == null;

        public static T FixNullObject<T>(this T thisValue) where T : class, new()
            => thisValue ?? new T();

        public static (bool Result, int Index, T Value) InX<T>(this T thisValue, IEnumerable<T> values)
        {
            int index = -1;
            foreach (T value in values)
            {
                index++;
                if (value.Equals(thisValue))
                {
                    return (true, index, value);
                }
            }
            return (false, -1, default);
        }

        public static (bool Result, int Index, T Value) InX<T>(this T thisValue, params T[] values)
            => InX(thisValue, values as IEnumerable<T>);

        public static bool In<T>(this T thisValue, IEnumerable<T> values)
            => thisValue.InX(values).Result;

        public static bool In<T>(this T thisValue, params T[] values)
            => thisValue.In(values as IEnumerable<T>);
    }
}
