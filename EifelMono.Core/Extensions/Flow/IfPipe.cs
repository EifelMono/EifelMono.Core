using System;

namespace EifelMono.Core.Extensions
{
    public class IfPipe<T> where T : IComparable
    {
        public T Value { get; set; }
        public bool Done { get; set; } = false;
    }
}
