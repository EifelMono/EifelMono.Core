using System;
using System.Threading;
using System.Threading.Tasks;

namespace EifelMono.Core.Extension
{
    public static class TasksExtension
    {
        public static Task AsTask(this CancellationTokenSource cancellationTokenSource, int maxWait)
        {
            return Task.Delay(maxWait, cancellationTokenSource.Token);
        }

        public static Task AsTask(this CancellationTokenSource cancellationTokenSource, uint maxWait)
        {
            return Task.Delay((int)maxWait, cancellationTokenSource.Token);
        }
    }
}
