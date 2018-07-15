using System.Diagnostics;

namespace EifelMono.Core.Log
{
    public class LogProxyDebug : LogProxy
    {
        public override void Log(LogExtension.Detail details)
        {
            Debug.WriteLine($"{details.Kind};\"{details.Message}\"");
        }
    }
}
