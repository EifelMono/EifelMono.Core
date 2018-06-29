using System;
using EifelMono.Core.Log;

namespace EifelMono.Core.System
{
    public class First
    {
        public bool State { get; set; }

        public bool IsFirst
        {
            get
            {
                if (State)
                {
                    State = false;
                    return true;
                }
                return false;
            }
        }
        public void Reset()
        {
            State = true;
        }
    }
}
