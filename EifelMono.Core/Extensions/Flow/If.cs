using System;

namespace EifelMono.Core.Extensions
{
    public static class FlowExtension
    {
        public static IfPipe<T> If<T>(this T value) where T : IComparable
        {
            return new IfPipe<T>
            {
                Value = value,
                Done = false
            };
        }

        public static IfPipe<T> Equal<T>(this IfPipe<T> pipe, T value, Action action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke();
            return pipe;
        }

        public static IfPipe<T> Equal<T>(this IfPipe<T> pipe, T value, Action<T> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke(value);
            return pipe;
        }

        public static IfPipe<T> Equal<T>(this IfPipe<T> pipe, T value, Action<IfPipe<T>, T> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.CompareTo(value) == 0)
                action?.Invoke(pipe, value);
            return pipe;
        }

        public static IfPipe<T> Default<T>(this IfPipe<T> pipe, Action action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            action?.Invoke();
            return pipe;
        }

        public static IfPipe<T> Default<T>(this IfPipe<T> pipe, Action<IfPipe<T>> action) where T : IComparable
        {
            if (pipe.Done)
                return pipe;
            action?.Invoke(pipe);
            return pipe;
        }

    }
}
