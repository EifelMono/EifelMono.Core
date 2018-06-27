using System;

namespace EifelMono.Core.Classes
{
    [Obsolete]
    public class First
    {
        private bool _Flag = true;

        public bool Flag { get => _Flag; set => _Flag = value; }

        public bool IsFirst
        {
            get
            {
                if (_Flag)
                {
                    _Flag = false;
                    return true;
                }
                return false;
            }
        }
    }

    [Obsolete]
    public class First<T> : First where T : IComparable
    {
        public First()
        {
            DefaultValue = default(T);
            Reset(true);
        }

        public First(T defaultValue)
        {
            DefaultValue = defaultValue;
            Reset(true);
        }

        public void Reset(bool withDefaultValue)
        {
            Flag = true;
            if (withDefaultValue)
                Value = DefaultValue;
        }

        public T DefaultValue { get; private set; }

        public T Value { get; set; }

        public bool IsFirstOrEqual(T value, bool setValue = true)
        {
            bool result = false;
            if (IsFirst || Value.CompareTo(value) == 0)
                result = true;
            if (setValue)
                Value = value;
            return result;
        }

        public bool IsFirstOrNotEqual(T value, bool setValue = true)
        {
            bool result = false;
            if (IsFirst || Value.CompareTo(value) != 0)
                result = true;
            if (setValue)
                Value = value;
            return result;
        }

        public bool IsFirstOrChanged(T value, bool setValue = true)
        {
            return IsFirstOrNotEqual(value, setValue);
        }
    }
}
