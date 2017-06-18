using System;

namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region If Keyword

        public static Pipe<T> If<T>(this T value)
        {
            return new Pipe<T>
            {
                Value = value
            };
        }

        public static Pipe<object> IfAsObject(this object value)
        {
            return new Pipe<object>
            {
                Value = value
            };
        }

        #endregion
    }
}
