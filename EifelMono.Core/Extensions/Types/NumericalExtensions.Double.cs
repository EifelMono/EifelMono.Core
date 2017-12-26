using System;

namespace EifelMono.Core.Extensions
{
    public static partial class NumericalExtensions
    {
        #region Math

        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        public static double Min(this double value, double otherValue)
        {
            return Math.Min(value, otherValue);
        }

        public static double Max(this double value, double otherValue)
        {
            return Math.Max(value, otherValue);
        }

        public static bool InRangeOffset(this double value, double baseValue, double offset)
        {
            return value.InRange(baseValue - offset, baseValue + offset);
        }

        #endregion
    }
}
