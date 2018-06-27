using System;
using System.Diagnostics;

namespace EifelMono.Core.Log {
    public class DebugLogProxy : LogProxy
    {
        public override void Log(LogExtension.Detail details)
        {
            base.Log(details);
        }
        //public void Log(Log.Detail details)
        //{
        //    Debug.WriteLine(details.ToString());
        //}
    }
}
 