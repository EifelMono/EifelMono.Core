using System;

namespace EifelMono.Core.Extension
{
    public static class PipeExtension
    {
        public static T Pipe<T>(this T thisValue, Action<T> action)
        {
            action(thisValue);
            return thisValue;
        }

        public static T Pipe<T>(this T thisValue, Func<T, T> action)
        {
            return action(thisValue);
        }
    }
}
