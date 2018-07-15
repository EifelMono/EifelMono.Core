using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.Core.Log
{
    public class LogProxyConsole : LogProxy
    {
        protected virtual void SetColors(LogKind logKind)
        {
            var backgroundColor = ConsoleColor.Black;
            var foregroundColor = ConsoleColor.White;
            switch (logKind)
            {
                case LogKind.Variable:
                    foregroundColor = ConsoleColor.Blue;
                    break;
                case LogKind.Debug:
                    backgroundColor = ConsoleColor.Gray;
                    break;
                case LogKind.Info:
                    foregroundColor = ConsoleColor.Green;
                    break;
                case LogKind.Trace:
                    break;
                case LogKind.Warning:
                    foregroundColor = ConsoleColor.Yellow;
                    break;
                case LogKind.Error:
                    foregroundColor = ConsoleColor.Red;
                    break;
                case LogKind.Exception:
                    backgroundColor = ConsoleColor.Red;
                    foregroundColor = ConsoleColor.White;
                    break;
            }
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

        protected virtual void ResetColors()
        {
            SetColors(LogKind.None);
        }
        public override void Log(LogExtension.Detail details)
        {
            if (details == null)
                return;
            SetColors(details.Kind);
            Console.WriteLine(details.ToCsvString(MessageOnly, ShortFileName));
            ResetColors();
        }
    }
}
