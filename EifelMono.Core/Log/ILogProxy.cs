using System;

namespace EifelMono.Core.Log {
    public interface ILogProxy
    {
        void Log(LogExtension.Detail details);

        bool FileNameOnly { get; set; }
    }
}
 