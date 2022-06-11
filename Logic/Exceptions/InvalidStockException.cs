using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public class InvalidStockException : Exception
    {
        public InvalidStockException(string message) : base(message) { }
    }
}
