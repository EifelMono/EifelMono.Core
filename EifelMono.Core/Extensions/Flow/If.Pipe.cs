using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Pipe
        public class Pipe<T>
        {
            public T Value { get; set; }
            public bool Done { get; set; } = false;
        }
        #endregion
    }
}
