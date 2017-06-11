using System;

namespace EifelMono.Core.Extensions
{
    public class LogProxy: ILogProxy
    {
        public bool FileNameOnly { get; set; } = true;

        public virtual void Log(Log.Detail details)
        {
        }
    }
}
