using System;

namespace Moodio
{
    public class RecordNotFoundException : Exception
    {
        RecordNotFoundException() { }

        RecordNotFoundException(string message) : base(message) { }
    }
}
