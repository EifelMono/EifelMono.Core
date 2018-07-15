using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EifelMono.Core.Binding
{
    public class BindingCollection<T> : ObservableCollection<T>
    {
        public BindingCollection<T> AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                this.Add(item);
            return this;
        }

        public void RefreshAll() => OnPropertyChanged(new global::System.ComponentModel.PropertyChangedEventArgs(string.Empty));
    }
}
