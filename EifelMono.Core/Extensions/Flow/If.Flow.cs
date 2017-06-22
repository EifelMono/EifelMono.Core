using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {

        public static Pipe<T> Condition<T>(this Pipe<T> pipe, Func<Pipe<T>, T, bool> conditionAction, Action<Pipe<T>, T> action)
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.Value is T value)
                if (pipe.SetBreak(conditionAction != null && conditionAction.Invoke(pipe, value)))
                    action?.Invoke(pipe, value);
            return pipe;
        }

        public static Pipe<T> Default<T>(this Pipe<T> pipe, Action action) where T : IComparable
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.SetBreak(true))
                action?.Invoke();
            return pipe;
        }

    }
}