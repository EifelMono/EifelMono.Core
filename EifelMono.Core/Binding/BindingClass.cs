using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using EifelMono.Core.Extension;

namespace EifelMono.Core.Binding
{
    public class BindingClass : INotifyPropertyChanged, IBindingClass
    {
        public BindingClass()
        {
            BindingProperties.ForEach(p => p.Owner = this);
        }

        #region BindingProperties
        protected List<BindingProperty> _BindingProperties = null;
        public List<BindingProperty> BindingProperties
        {
            get => _BindingProperties ?? (_BindingProperties = new List<BindingProperty>())
                .Pipe((bindingProperties) =>
                {
                    this.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(property => property.PropertyType.IsSubclassOf(typeof(BindingProperty)))
                        .ForEach((property) =>
                        {
                            var bindingProperty = (BindingProperty)property.GetValue(this, null);
                            if (bindingProperty != null && !bindingProperties.Contains(bindingProperty))
                                bindingProperties.Add(bindingProperty);
                        });
                });
        }
        #endregion

        #region Bindings
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            (PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public void RefreshAll() => OnPropertyChanged(string.Empty);
        #endregion

        #region property with backing field
        protected virtual bool SetProperty<T>(
                   ref T backingValue, T value,
                   [CallerMemberName]string propertyName = "",
                   Action onChanged = null,
                   Func<T, T, bool> validateValue = null)
        {
            //if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingValue, value))
                return false;

            //if value changed but didn't validate
            if (validateValue != null && !validateValue(backingValue, value))
                return false;

            backingValue = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }

    public class BindingClass<T> : BindingClass
    {
        public T Owner { get; set; }

        public BindingClass<T> SetOwner(T owner)
        {
            Owner = owner;
            return this;
        }
    }
}
