using EifelMono.Core.System;
using System.Threading;
using System.Threading.Tasks;


namespace EifelMono.Core.Extension
{
    public static class TasksExtension
    {
        public static Task AsTask(this CancellationTokenSource thisValue, int maxWait)
            => EmcTask.Delay(maxWait, thisValue.Token);

        public static Task AsTask(this CancellationTokenSource thisValue, uint maxWait)
            => EmcTask.Delay((int)maxWait, thisValue.Token);
    }
}
