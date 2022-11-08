using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class StaticRecordException : Exception
    {
        public StaticRecordException(string message)
            : base(message)
        {
        }
    }
}
