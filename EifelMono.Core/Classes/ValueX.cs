using System;
using EifelMono.Core.Extensions;

namespace EifelMono.Core
{
    public class ValueX
    {
        public object Value { get; set; } = default(object);
        public object LastValue { get; set; } = default(object);
        public bool IsFirst { get; set; } = true;
        public bool IsInit { get; set; } = false;
    }

    public class ValueX<T> : ValueX where T : IComparable
    {
        public ValueX()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    base.Value = "";
                    base.LastValue = "";
                    break;
                case TypeCode.Object:
                    base.Value = null;
                    base.LastValue = null;
                    break;
                default:
                    base.Value = default(T);
                    base.LastValue = default(T);
                    break;
            }
            IsFirst = true;
        }

        public ValueX(Action<T> doInit) : this()
        {
            DoInit= doInit;
        }

        public delegate void DelegateOnValueChanged(ValueX<T> valueX);
        public event DelegateOnValueChanged OnValueXChanged;

        protected Action<T> DoInit = null;
        protected void CheckInit()
        {
            if (!IsInit)
            {
                if (Value.Equals(null))
                {
                    if (Type.GetTypeCode(typeof(T)) == TypeCode.Object)
                        Value = (T)Activator.CreateInstance(typeof(T));
                }
                DoInit?.Invoke(Value);
                IsInit = true;
            }
        }

        public new T Value { get { return GetValue(); } set { SetValue(value); } }

        protected virtual T GetValue()
        {
            CheckInit();
            return (T)base.Value;
        }

        protected void DoChange(T value)
        {
            base.LastValue = Value;
            base.Value = value;
            OnValueXChanged?.Invoke(this);
            IsFirst = false;
        }

        protected virtual void SetValue(T value)
        {
            CheckInit();
            try
            {
                var tValue = (T)base.Value;
                if (!tValue.Equals(null))
                {
                    if (tValue.CompareTo(value) != 0)
                        DoChange(value);
                }
                else
                {
                    if (!value.Equals(null))
                        DoChange(value);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }

        public new T LastValue { get { return (T)base.LastValue; } set { base.LastValue = value; } }

        public bool CheckFirst(T value)
        {
            var isFirst = IsFirst;
            Value = value;
            return isFirst;
        }
    }
}
