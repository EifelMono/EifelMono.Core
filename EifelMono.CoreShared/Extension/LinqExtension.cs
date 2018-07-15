using System;
using System.Collections.Generic;
using System.Linq;

namespace EifelMono.Core.Extension
{
    public static class LinqExtension
    {
        #region Enumerable ForEach  
        public static void ForEach<T>(this IEnumerable<T> thisValue, Action<T> action)
        {
            if (thisValue == null)
            {
                throw new NullReferenceException();
            }

            if (action == null)
            {
                throw new NullReferenceException();
            }

            foreach (T value in thisValue)
            {
                action(value);
            }
        }
        #endregion

        #region ForEachAsList
        public static void ForEachAsList<T>(this IEnumerable<T> thisValue, Action<T, int, int> action)
        {
            if (thisValue == null)
            {
                throw new NullReferenceException();
            }

            if (action == null)
            {
                throw new NullReferenceException();
            }

            List<T> thisList = thisValue.ToList();
            for (int index = 0; index < thisList.Count; index++)
            {
                action(thisList[index], index, thisList.Count);
            }
        }

        public static void ForEachAsList<T>(this IEnumerable<T> thisValue, Action<T> action)
             => thisValue.ForEachAsList((item, index, count) => action(item));

        #endregion

        #region ForEachAsReverseList 

        public static void ForEachAsReverseList<T>(this IEnumerable<T> thisValue, Action<T, int, int> action)
        {
            if (thisValue == null)
            {
                throw new NullReferenceException();
            }

            if (action == null)
            {
                throw new NullReferenceException();
            }

            List<T> thisList = thisValue.ToList();
            for (int index = thisList.Count - 1; index > 0; index--)
            {
                action(thisList[index], index, thisList.Count);
            }
        }

        public static void ForEachAsReverseList<T>(this IEnumerable<T> thisValue, Action<T> action)
            => thisValue.ForEachAsReverseList((item, index, count) => action(item));
        #endregion
    }
}
