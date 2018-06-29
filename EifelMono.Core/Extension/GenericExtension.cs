using System;
using System.Collections.Generic;

namespace EifelMono.Core.Extension
{
    public static class GenericExtension
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
                action(item);
        }
    }
}
