using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message)
            : base(message)
        {
        }
    }
}
