using System;

namespace Moodio
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() { }

        public RecordNotFoundException(string message) : base(message) { }
    }
}
