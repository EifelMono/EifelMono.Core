using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Equal's

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke();
            return pipe;
        }

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action<Pipe<T>, T> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion

        #region Default's

        public static Pipe<T> Default<T>(this Pipe<T> pipe, Action action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            action?.Invoke();
            return pipe;
        }

        public static Pipe<T> Default<T>(this Pipe<T> pipe, Action<Pipe<T>> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            action?.Invoke(pipe);
            return pipe;
        }
        #endregion
    }
}