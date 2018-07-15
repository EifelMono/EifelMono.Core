using System;
using System.Runtime.CompilerServices;

namespace EifelMono.Core.Log {
    public static partial class LogExtension
    {
        #region Proxy, gobal call

        public static ILogProxy Proxy { get; set; } = new LogProxyDebug();

        public static void UseAsNewProxy(this ILogProxy logProxy)
        {
            Proxy = logProxy;
        }

        public static Detail ProxyLog(Detail detail,
                                      Detail parentDetail,
                                      string message,
                                      LogKind kind,
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
        public static Detail LogTrace(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, LogKind.Trace, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Info
       

        public static Detail LogInfo(this string message,
                                     Detail parentDetail = null,
                                     [CallerMemberName] string callerMemberName = "",
                                     [CallerLineNumber] int callerLineNumber = -1,
                                     [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, LogKind.Info, callerMemberName, callerLineNumber, callerFilePath);
        }

        #endregion

        #region Log Value
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
            return ProxyLog(null, parentDetail, valueString, LogKind.Variable, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Error
        public static Detail LogError(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, LogKind.Error, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Warning

        public static Detail LogWarning(this string message,
                                        Detail parentDetail = null,
                                        [CallerMemberName] string callerMemberName = "",
                                        [CallerLineNumber] int callerLineNumber = -1,
                                        [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, LogKind.Warning, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Debug
        public static Detail LogDebug(this string message,
                                      Detail parentDetail = null,
                                      [CallerMemberName] string callerMemberName = "",
                                      [CallerLineNumber] int callerLineNumber = -1,
                                      [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(null, parentDetail, message, LogKind.Debug, callerMemberName, callerLineNumber, callerFilePath);
        }
        #endregion

        #region Log Exception
        public static Detail LogException(this Exception ex,
                                         Detail parentDetail = null,
                                         [CallerMemberName] string callerMemberName = "",
                                         [CallerLineNumber] int callerLineNumber = -1,
                                         [CallerFilePath] string callerFilePath = "")
        {
            return ProxyLog(new ExDetail { Ex = ex }, parentDetail, ex.ToString(), LogKind.Exception, callerMemberName, callerLineNumber, callerFilePath);
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
