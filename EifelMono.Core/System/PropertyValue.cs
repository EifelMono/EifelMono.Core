using System;
using System.Collections.Generic;
using System.Text;
using EifelMono.Core.Extension;
using EifelMono.Core.Log;

namespace EifelMono.Core.System
{
    public class PropertyValue<T> : First where T : IComparable
    {
        public PropertyValue()
        {
        }
      
        public T Value { get; set; } = default;
        public T LastValue { get; set; } = default;

        public void Reset(T value)
        {
            LastValue = Value;
            Value = value;
            State = false;
        }
        public bool IsChanged(T value)
        {
            try
            {
                if (Value.CompareTo(value) != 0)
                {
                    LastValue = Value;
                    Value = value;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ex.LogException();
                LastValue = Value;
                Value = value;
                return true;
            }
        }
        public bool IsEqual(T value)
            => IsChanged(value);
        public bool IsNotEqual(T value)
            => !IsChanged(value);

        public bool IsFirstOrChanged(T value)
        {
            try
            {
                if (IsFirst || (Value.CompareTo(value) != 0))
                {
                    LastValue = Value;
                    Value = value;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ex.LogException();
                State = true;
                LastValue = Value;
                Value = value;
                return true;
            }
        }
        public bool IsFirstOrEqual(T value)
            => !IsFirstOrChanged(value);
        public bool IsFirstOrNotEqual(T value)
            => IsFirstOrChanged(value);
    }
}
