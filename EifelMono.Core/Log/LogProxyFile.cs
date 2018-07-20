using System;
using System.IO;
using System.Threading.Tasks;
using EifelMono.Core.Extension;
using EifelMono.Core.System;

namespace EifelMono.Core.Log
{
    public class LogProxyFile : LogProxy
    {
        public string _RootFolder;
        public string RootFolder
        {
            get => _RootFolder ?? (_RootFolder =
                EmcPath.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                        "EifelMono",
                        Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]))
                .Pipe(p => EmcDirectory.EnsureDirectoryExists(p)));
            set
            {
                _RootFolder = value;
                EmcDirectory.EnsureDirectoryExists(_RootFolder);
            }
        }

        protected string Filename(LogExtension.Detail details)
            => EmcPath.Combine(RootFolder, $"{details.TimeStamp.ToString("yyyyMMdd")}.{details.Kind}.log");

        public object LockObject = new object();
        public override void Log(LogExtension.Detail details)
        {
            if (RootFolder == null)
                return;
            EmcTask.Run(() =>
            {
                lock (LockObject)
                {
                    EmcFile.AppendAllText(Filename(details), details.ToCsvString());
                }
            });
        }
    }
}
