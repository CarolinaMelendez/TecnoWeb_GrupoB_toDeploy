using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(String message) : base(message) { }
    }
}
