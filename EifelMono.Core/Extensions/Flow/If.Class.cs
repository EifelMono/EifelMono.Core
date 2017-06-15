using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Is
        public static Pipe<T> Is<T>(this Pipe<T> pipe, T value, Action action) where T : class
        {
            if (pipe.Done)
                return pipe;
            if (value is T newValue)
            {
                pipe.Done = true;
                action?.Invoke();
            }
            return pipe;
        }

        public static Pipe<T> Is<T>(this Pipe<T> pipe, T value, Action<Pipe<T>, T> action) where T : class
        {
            if (pipe.Done)
                return pipe;
            if (value is T newValue)
            {
                pipe.Done = true;
                action?.Invoke(pipe, newValue);
            }
            return pipe;
        }
        #endregion

        #region IsAndEqual

        public static Pipe<T> IsAndEqual<T>(this Pipe<T> pipe, T value, Func<T, bool> equalAction, Action action) where T : class
        {
            if (pipe.Done)
                return pipe;
            if (value is T newValue)
                if (equalAction != null && equalAction.Invoke(newValue))
                {
                    pipe.Done = true;
                    action?.Invoke();
                }
            return pipe;
        }

        public static Pipe<T> IsAndEqual<T>(this Pipe<T> pipe, T value, Func<T, bool> equalAction, Action<Pipe<T>, T> action) where T : class
        {
            if (pipe.Done)
                return pipe;
            if (value is T newValue)
                if (equalAction != null && equalAction.Invoke(newValue))
                {
                    pipe.Done = true;
                    action?.Invoke(pipe, newValue);
                }
            return pipe;
        }
        #endregion
    }
}
