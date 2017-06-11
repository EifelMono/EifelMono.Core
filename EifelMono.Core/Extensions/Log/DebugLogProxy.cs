using System;
using System.Diagnostics;

namespace EifelMono.Core.Extensions
{
    public class DebugLogProxy : LogProxy
    {
        public override void Log(Log.Detail details)
        {
            base.Log(details);
        }
        //public void Log(Log.Detail details)
        //{
        //    Debug.WriteLine(details.ToString());
        //}
    }
}
