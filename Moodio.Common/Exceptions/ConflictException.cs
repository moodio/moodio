using System;
using System.Collections.Generic;
using System.Text;

namespace Moodio.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base() { }

        public ConflictException(string message) : base(message) { }
    }
}
