using System;
namespace EifelMono.Core.Classes
{
    [Obsolete]
    public interface IOnPropertyChanged
    {
        void OnPropertyChanged(string propertyName = null);
    }
}
