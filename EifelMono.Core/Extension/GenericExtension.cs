using System;
using System.Collections.Generic;
using System.Linq;

namespace EifelMono.Core.Extension
{
    public static class GenericExtension
    {
        public static void ForEach<T>(this IEnumerable<T> thisValue, Action<T> action)
        {
            var listItems = thisValue.ToList();
            foreach (var item in listItems)
                action(item);
        }
        public static void ForEach<T>(this IEnumerable<T> thisValue, Action<T, int, int> action)
        {
            var listItems = thisValue.ToList();
            var index = 0;
            foreach (var item in listItems)
                action(item, index++, listItems.Count);
        }
    }
}
