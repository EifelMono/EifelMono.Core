using System;
namespace EifelMono.Core.Classes
{
    public class Toogle
    {
        private bool _Flag = false;

        public bool Flag { get => _Flag; set => _Flag = value; }

        public bool IsToogle
        {
            get
            {
                _Flag = !_Flag;
                return _Flag;
            }
        }
    }
}
