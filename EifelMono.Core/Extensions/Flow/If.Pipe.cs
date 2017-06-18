using System;
namespace EifelMono.Core.Extensions
{
    public static partial class IfExtension
    {
        #region Pipe
        public class Pipe<T>
        {
            public T Value { get; set; }
            internal bool _Break { get; set; } = false;

            public bool IsBreak
            {
                get
                {
                    return _Break;
                }
            }

            public bool SetBreak(bool value)
            {
                if (value)
                    _Break = value;
                return _Break;
            }


            public void Break()
            {
                _Break = true;
            }

            public void Continue()
            {
                _Break = false;
            }
        }
        #endregion
    }
}
