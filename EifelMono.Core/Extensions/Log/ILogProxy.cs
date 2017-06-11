using System;

namespace EifelMono.Core.Extensions
{
    public interface ILogProxy
    {
        void Log(Log.Detail details);

        bool FileNameOnly { get; set; }
    }
}
