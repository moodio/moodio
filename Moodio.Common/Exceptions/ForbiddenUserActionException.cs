using System;
using System.Collections.Generic;
using System.Text;

namespace Moodio.Exceptions
{
    public class ForbiddenUserActionException : Exception
    {
        public ForbiddenUserActionException() : base() { }
        
        public ForbiddenUserActionException(string message) : base(message) { }
    }
}
