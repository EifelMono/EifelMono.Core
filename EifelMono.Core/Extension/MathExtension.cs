using System;

namespace EifelMono.Core.Extension
{
    public static class MathExtension
    {
        public static T Abs<T>(this T thisValue)
        {
            return default(T);
        }

        public static void Test()
        {
            var x = 1;
            var y = x.Abs<int>();
        }
    }
}
