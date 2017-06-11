using System;

namespace EifelMono.Core.Extensions
{
    public static class FlowExtension
    {
        #region Pipe
        public class Pipe<T> where T : IComparable
        {
            public T Value { get; set; }
            public bool Done { get; set; } = false;
        }
        #endregion

        #region If Keyword

        public static Pipe<T> If<T>(this T value) where T : IComparable
        {
            return new Pipe<T>
            {
                Value = value,
                Done = false
            };
        }
        #endregion

        #region Equal's

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke();
            return pipe;
        }

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action<T> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke(value);
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
