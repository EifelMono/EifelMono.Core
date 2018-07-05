using System;

namespace EifelMono.Core.Log {
    public interface ILogProxy
    {
        void Log(LogExtension.Detail details);

        bool ShortFileName { get; set; }

        bool MessageOnly { get; set; }
    }
}
