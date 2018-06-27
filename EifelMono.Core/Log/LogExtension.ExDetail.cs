using System;

namespace EifelMono.Core.Log {
    public static partial class LogExtension
    {
        public class ExDetail : Detail
        {
            /// <summary>
            /// Exception of this Log Detail
            /// </summary>
            /// <value>The ex.</value>
            public Exception Ex { get; set; }
        }
    }
}
 