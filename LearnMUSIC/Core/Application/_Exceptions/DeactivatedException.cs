using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class DeactivatedException : Exception
    {
        public DeactivatedException(string message)
            : base(message)
        {
        }
    }
}
