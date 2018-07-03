using System;
using System.Threading;
using System.Threading.Tasks;

namespace EifelMono.Core.Extension
{
    public static class TasksExtension
    {
        public static Task AsTask(this CancellationTokenSource thisValue, int maxWait)
        {
            return Task.Delay(maxWait, thisValue.Token);
        }

        public static Task AsTask(this CancellationTokenSource thisValue, uint maxWait)
        {
            return Task.Delay((int)maxWait, thisValue.Token);
        }
    }
}
