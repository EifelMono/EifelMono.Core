using System;
using System.Runtime.CompilerServices;
using EifelMono.Core.System;

namespace EifelMono.Core.Binding
{
    public class BindingProperty
    {
        public string PropertyName { get; set; }
        public IBindingClass Owner { get; set; } = null;
        public void OnPropertyChanged(string propertyName = null) =>
            Owner?.OnPropertyChanged(propertyName ?? PropertyName);
        public void RefreshAll() => OnPropertyChanged(string.Empty);
    }

    public class BindingProperty<T> : BindingProperty where T : IComparable
    {
        public BindingProperty([CallerMemberName]string propertyName = "")
        {
            PropertyName = propertyName;
        }
        protected PropertyValue<T> BindingValue = new PropertyValue<T>();
        public T Value
        {
            get => BindingValue.Value;
            set
            {
                if (BindingValue.IsFirstOrChanged(value))
                {
                    OnPropertyChanged();
                    _OnChanged?.Invoke(this);
                }
            }
        }
        public BindingProperty<T> SetValue(T setValue)
        {
            Value = setValue;
            return this;
        }

        public BindingProperty<T> Default(T defaultValue = default) => SetValue(defaultValue);

        public delegate void OnChangedAction(BindingProperty<T> self);
        protected OnChangedAction _OnChanged { get; set; }

        public BindingProperty<T> DoOnChanged(OnChangedAction onChanged)
        {
            _OnChanged = onChanged;
            return this;
        }
        public BindingProperty<T> SetOwner(IBindingClass owner)
        {
            Owner = owner;
            return this;
        }

        #region Overloading
        // not possible because no name
        //public static implicit operator BindingProperty<T>(T value)
        //{
        //    return new BindingProperty<T>().Default(value);
        //}

        // public static implicit operator T(BindingProperty<T> value)
        // {
        //     return value.Value;
        // }
        #endregion
    }
}
