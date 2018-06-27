﻿using System;
using System.IO;

namespace EifelMono.Core.Log {
    public static partial class LogExtension
    {
        public class Detail
        {
            /// <summary>
            /// Gets or sets the time stamp.
            /// </summary>
            /// <value>The time stamp.</value>
            public DateTime TimeStamp { get; set; } = DateTime.Now;

            /// <summary>
            /// Kind of this Log Detail
            /// </summary>
            /// <value>The kind.</value>
            public LogKind Kind { get; set; }

            /// <summary>
            /// Message of this Log Detail
            /// </summary>
            /// <value>The message.</value>
            public string Message { get; set; }

            /// <summary>
            /// CallerMemberName of this Log Detail
            /// </summary>
            /// <value>The name of the caller member.</value>
            public string CallerMemberName { get; set; }

            /// <summary>
            /// CallerLineNumber of this Log Detail
            /// </summary>
            /// <value>The caller line number.</value>
            public int CallerLineNumber { get; set; }

            /// <summary>
            /// CallerLineNumber of this Log Detail
            /// </summary>
            /// <value>The caller file path.</value>
            public string CallerFilePath { get; set; }

            public UInt64 Id { get; set; } = NextId;
            public UInt64 ParentId { get; set; } = 0;

            #region Id Helps
            private static UInt64 _NextId = 0;
            private static object NextIdLockObject = new object();
            public static UInt64 NextId
            {
                get
                {
                    UInt64 id = 0;
                    lock (NextIdLockObject)
                    {
                        id = _NextId + 1;
                        _NextId = id;
                    }
                    return id;
                }
            }
            #endregion

            public virtual string ToCsvString()
            {
                return string.Format($"[{Id}/{ParentId}];{Kind};{Message};[{CallerMemberName}:{CallerLineNumber}:{Path.GetFileName(CallerFilePath)}]");
            }

            public override string ToString()
            {
                return string.Format($"{TimeStamp}, [{Id}/{ParentId}];{Kind};{Message};[{CallerMemberName}:{CallerLineNumber}:{Path.GetFileName(CallerFilePath)}]");
            }
        }
    }
}
 