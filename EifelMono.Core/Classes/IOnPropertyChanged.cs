using System;
namespace EifelMono.Core
{
    public interface IOnPropertyChanged
    {
        void OnPropertyChanged(string propertyName = null);
    }
}
