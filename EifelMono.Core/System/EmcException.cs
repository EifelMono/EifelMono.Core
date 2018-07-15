using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.Core.System
{
    public class EmcException : Exception
    {
        public EmcException() : base()
        {

        }

        public EmcException(string message) : base(message)
        {

        }
    }
}
