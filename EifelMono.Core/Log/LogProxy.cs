namespace EifelMono.Core.Log
{
    public abstract class LogProxy : ILogProxy
    {
        public bool FileNameOnly { get; set; } = true;

        public abstract void Log(LogExtension.Detail details);
    }
}
