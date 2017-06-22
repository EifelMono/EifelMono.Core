using System;
namespace EifelMono.Core.Classes
{
    public class Changed<T> where T : IComparable
    {
        public Changed()
        {
        }

        public Changed(T value)
        {
            Value = value;

        }

        T _Value = default(T);

        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        };

        public bool IsChanged { get; set; } = false;


    }
}
