using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Equal's

        public static Pipe<T> Equal<T>(this Pipe<T> pipe, T value, Action<Pipe<T>> action) where T : IComparable
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.Value.CompareTo(value) == 0)
                action?.Invoke(pipe.Break());
            return pipe;
        }
        #endregion
    }
}