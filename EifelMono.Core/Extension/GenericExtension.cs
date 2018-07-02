using System;
using System.Collections.Generic;
using System.Linq;

namespace EifelMono.Core.Extension
{
    public static class GenericExtension
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            var listItems = items.ToList();
            foreach (var item in listItems)
                action(item);
        }
        public static void ForEach<T>(this IEnumerable<T> items, Action<T, int, int> action)
        {
            var listItems = items.ToList();
            var index = 0;
            foreach (var item in listItems)
                action(item, index++, listItems.Count);
        }
    }
}
