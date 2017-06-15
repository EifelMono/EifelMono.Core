using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region StartsWith

        public static Pipe<string> StartsWith(this Pipe<string> pipe, string value, Action action)
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.StartsWith(value, StringComparison.Ordinal))
                action?.Invoke();
            return pipe;
        }

        public static Pipe<string> StartWith(this Pipe<string> pipe, string value, Action<Pipe<string>, string> action)
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.StartsWith(value, StringComparison.Ordinal))
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion

        #region Contains

        public static Pipe<string> Contains(this Pipe<string> pipe, string value, Action action)
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.Contains(value))
                action?.Invoke();
            return pipe;
        }

        public static Pipe<string> Contains(this Pipe<string> pipe, string value, Action<Pipe<string>, string> action)
        {
            if (pipe.Done)
                return pipe;
            if (pipe.Done = pipe.Value.Contains(value))
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion
    }
}
