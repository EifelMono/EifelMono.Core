using System;
using System.Runtime.CompilerServices;

namespace EifelMono.Core.Extensions
{
    public static partial class Log
    {
        #region Proxy, gobal call

        public static ILogProxy Proxy { get; set; } = new DebugLogProxy();

        public static Detail ProxyLog(Detail detail,
                                      Detail parentDetail,
                                      string message,
                                      Kind kind,
                                      string callerMemberName,
                                      int callerLineNumber,
                                      string callerFilePath)
        {
            if (detail == null)
                detail = new Detail();
            if (parentDetail != null)
                detail.ParentId = parentDetail.Id;
            detail.Kind = kind;
            detail.Message = message;
            detail.CallerMemberName = callerMemberName;
            detail.CallerLineNumber = callerLineNumber;
            detail.CallerFilePath = callerFilePath;
            Proxy?.Log(detail);
            return detail;
        }
        #endregion

        #region Trace
        public partial class Kind : EnumOf<string>
        {
            public static Kind Trace = new Kind { Value = $"{nameof(Trace)}" };
        }

        public static Detail LogTrace(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, Kind.Trace, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Info
        public partial class Kind : EnumOf<string>
        {
            public static Kind Info = new Kind { Value = $"{nameof(Info)}" };
        }

        public static Detail LogInfo(this string message,
                                     Detail parentDetail = null,
                                     [CallerMemberName] string callerMemberName = "",
                                     [CallerLineNumber] int callerLineNumber = -1,
                                     [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, Kind.Info, callerMemberName, callerLineNumber, callerFilePath);
        }

        #endregion

        #region Log Value
        public partial class Kind : EnumOf<string>
        {
            public static Kind Variable = new Kind { Value = $"{nameof(Variable)}" };
        }

        public static Detail LogVariable<T>(this T value,
                                            string message = "",
                                            Detail parentDetail = null,
                                            [CallerMemberName] string callerMemberName = "",
                                            [CallerLineNumber] int callerLineNumber = -1,
                                            [CallerFilePath] string callerFilePath = "")
        {
            string valueString = "";
            try
            {
                valueString = value?.ToString();
            }
            catch
            {
                valueString = "null";
            }
            if (message == null)
                valueString = $"Value:{callerMemberName}={valueString}";
            else
                try
                {
                    if (message.Contains("{0}"))
                        valueString = string.Format(message, value);
                    else
                        valueString = message;
                }
                catch (Exception ex)
                {
                    valueString = ex.ToString();
                }
            return ProxyLog(null, parentDetail, valueString, Kind.Variable, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Error
        public partial class Kind : EnumOf<string>
        {
            public static Kind Error = new Kind { Value = $"{nameof(Error)}" };
        }
        public static Detail LogError(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, Kind.Error, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Warning
        public partial class Kind : EnumOf<string>
        {
            public static Kind Warning = new Kind { Value = $"{nameof(Warning)}" };
        }

        public static Detail LogWarning(this string message,
                                        Detail parentDetail = null,
                                        [CallerMemberName] string callerMemberName = "",
                                        [CallerLineNumber] int callerLineNumber = -1,
                                        [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, Kind.Warning, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Debug
        public partial class Kind : EnumOf<string>
        {
            public static Kind Debug = new Kind { Value = $"{nameof(Debug)}" };
        }

        public static Detail LogDebug(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, Kind.Debug, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Exception
        public partial class Kind : EnumOf<string>
        {
            public static Kind Exception = new Kind { Value = $"{nameof(Exception)}" };
        }

        public static Detail LogException(this Exception ex,
                                         Detail parentDetail = null,
                                         [CallerMemberName] string callerMemberName = "",
                                         [CallerLineNumber] int callerLineNumber = -1,
                                         [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(new ExDetail { Ex = ex }, parentDetail, ex.ToString(), Kind.Exception, callerMemberName, callerLineNumber, callerFilePath);
        }

        [Obsolete("Please use LogException for further calls")]
        public static Detail HandleException(this Exception ex,
                                             ExDetail exDetail = null,
                                             [CallerMemberName] string callerMemberName = "",
                                             [CallerLineNumber] int callerLineNumber = -1,
                                             [CallerFilePath] string callerFilePath = "")
        {
            return ex.LogException(exDetail, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion
    }
}
