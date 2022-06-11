using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(String message) : base(message) { }
    }
}
