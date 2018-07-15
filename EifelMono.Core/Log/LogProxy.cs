namespace EifelMono.Core.Log
{
    public abstract class LogProxy : ILogProxy
    {
        public bool ShortFileName { get; set; } = true;

        public bool MessageOnly { get; set; } = true;

        public abstract void Log(LogExtension.Detail details);
    }
}
