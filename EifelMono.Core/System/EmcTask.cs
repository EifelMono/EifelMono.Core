using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EifelMono.Core.System
{
    public class EmcTask : Task
    {
        public EmcTask(Action action): base(action)
        {
        }

        public EmcTask(Action action, CancellationToken cancellationToken): base(action, cancellationToken)
        {
        }
#if NET40
        public static Task Delay(TimeSpan dueTime)
            => TaskEx.Delay(dueTime);
        public static Task Delay(TimeSpan dueTime, CancellationToken cancellationToken)
            => TaskEx.Delay(dueTime, cancellationToken);

        public static Task Delay(int dueTime)
            => TaskEx.Delay(dueTime);
        public static Task Delay(int dueTime, CancellationToken cancellationToken)
            => TaskEx.Delay(dueTime, cancellationToken);

        public static Task Run(Action action)
            => TaskEx.Run(action);
        public static Task<TResult> Run<TResult>(Func<TResult> function)
            => TaskEx.Run(function);
#endif
    }


    public class EmcTask<TResult> : EmcTask
    {
        public EmcTask(Action action) : base(action)
        {
        }

        public EmcTask(Action action, CancellationToken cancellationToken) : base(action, cancellationToken)
        {
        }
    }
}
