using System;
namespace EifelMono.Core.Extensions
{
    public static partial class NumericalExtensions
    {
        #region Math

        public static double Abs(this int value)
        {
            return Math.Abs(value);
        }

        public static double Min(this int value, int otherValue)
        {
            return Math.Min(value, otherValue);
        }

        public static double Max(this int value, int otherValue)
        {
            return Math.Max(value, otherValue);
        }

        public static bool InRangeOffset(this int value, int baseValue, int offset)
        {
            return value.InRange(baseValue - offset, baseValue + offset);
        }

        public static int ToInt(this string value)
        {
            Int32 result = 0;
            Int32.TryParse(value, out result);
            return result;
        }
        #endregion
    }
}
