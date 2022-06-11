using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class PriceServiceException : Exception
    {
        public PriceServiceException(String message) : base(message) { }
    }
}