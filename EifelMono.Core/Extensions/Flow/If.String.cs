using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Contains

        public static Pipe<string> Contains(this Pipe<string> pipe, string value, Action<Pipe<string>, string> action= null)
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.IsThisBreak(pipe.Value.Contains(value)))
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion

        #region StartsWith

        public static Pipe<string> StartWith(this Pipe<string> pipe, string value, Action<Pipe<string>, string> action= null)
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.IsThisBreak(pipe.Value.StartsWith(value, StringComparison.Ordinal)))
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion

        #region EndsWith

        public static Pipe<string> EndsWith(this Pipe<string> pipe, string value, Action<Pipe<string>, string> action= null)
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.IsThisBreak(pipe.Value.EndsWith(value, StringComparison.Ordinal)))
                action?.Invoke(pipe, value);
            return pipe;
        }
        #endregion
    }
}
