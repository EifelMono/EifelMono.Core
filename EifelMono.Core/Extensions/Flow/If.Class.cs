using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region AsObject
        public static Pipe<object> AsObject<T>(this Pipe<T> pipe) where T : class
        {
            return new Pipe<object>()
            {
                _Break = pipe._Break,
                Value = (object)pipe.Value
            };
        }
        #endregion

        #region Is
        public static Pipe<object> Is<T>(this Pipe<object> pipe, Action<Pipe<object>, T> action) where T : class
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.Value is T value)
            {
                pipe.BreakOnCondition(true);
                action?.Invoke(pipe, value);
            }
            return pipe;
        }

        public static Pipe<object> IsCondition<T>(this Pipe<object> pipe, Func<Pipe<object>, T, bool> conditionAction, Action<Pipe<object>, T> action) where T : class
        {
            if (pipe.IsBreak)
                return pipe;
            if (pipe.Value is T value)
            {
                if (conditionAction(pipe, value))
                {
                    pipe.BreakOnCondition(true);
                    action?.Invoke(pipe, value);  
                }

            }
            return pipe;
        }
        #endregion
    }
}
