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
            MvvmProperties.ForEach(p => p.ParentBindingObject = this);
        }

        #region MvvmProperties
        protected List<BindingProperty> _MvvmProperties = null;
        public List<BindingProperty> MvvmProperties
        {
            get
            {
                return _MvvmProperties ?? (_MvvmProperties = new List<BindingProperty>()).Pipe((_mvvmProperties) =>
                {
                    var properties = this.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => x.PropertyType.IsSubclassOf(typeof(BindingProperty)));
                    foreach (var p in properties)
                    {
                        var identifier = (BindingProperty)(p.GetValue(this, null));
                        if (identifier != null)
                            if (!_MvvmProperties.Contains(identifier))
                                _MvvmProperties.Add(identifier);
                    }

                });
            }
        }
        #endregion

        #region Bindings
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            (PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public void RefreshAll() => OnPropertyChanged(string.Empty);
        #endregion
    }

    public class BindingClass<T> : BindingClass
    {
        public T Owner { get; set; }
    }
}
