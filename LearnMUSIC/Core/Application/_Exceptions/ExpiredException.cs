using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class ExpiredException : Exception
    {
        public ExpiredException(string message)
            : base(message)
        {
        }
    }
}
