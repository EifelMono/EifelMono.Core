using System;
using System.Collections.Generic;

namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Equal

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action<Pipe<T>> action) where T : IComparable
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.IsThisBreak(pipe.Value.CompareTo(value) == 0))
                action?.Invoke(pipe);
            return pipe;
        }
        #endregion

        #region In

        public static Pipe<T> In<T>(this Pipe<T> pipe, IEnumerable<T> values, Action<Pipe<T>, T> action) where T : IComparable
        {
            if (pipe.IsBreak)
                return pipe;
            foreach (var value in values)
            {
                if (pipe.IsThisBreak(pipe.Value.CompareTo(value) == 0))
                    action?.Invoke(pipe, value);
                if (pipe.IsBreak)
                    return pipe;
            }
            return pipe;
        }

        public static Pipe<T> In<T>(this Pipe<T> pipe, T value1, Action<Pipe<T>, T> action) where T : IComparable
        {
            return In(pipe, new List<T> { value1 }, action);
        }
        public static Pipe<T> In<T>(this Pipe<T> pipe, T value1, T value2, Action<Pipe<T>, T> action) where T : IComparable
        {
            return In(pipe, new List<T> { value1, value2 }, action);
        }
        public static Pipe<T> In<T>(this Pipe<T> pipe, T value1, T value2, T value3, Action<Pipe<T>, T> action) where T : IComparable
        {
            return In(pipe, new List<T> { value1, value2, value3 }, action);
        }
        public static Pipe<T> In<T>(this Pipe<T> pipe, T value1, T value2, T value3, T value4, Action<Pipe<T>, T> action) where T : IComparable
        {
            return In(pipe, new List<T> { value1, value2, value3, value4 }, action);
        }
        public static Pipe<T> In<T>(this Pipe<T> pipe, T value1, T value2, T value3, T value4, T value5, Action<Pipe<T>, T> action) where T : IComparable
        {
            return In(pipe, new List<T> { value1, value2, value3, value4, value5 }, action);
        }

        #endregion
    }
}