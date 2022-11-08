using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class EditNotAllowedException : Exception
    {
        public EditNotAllowedException(string message)
            : base(message)
        {
        }
    }
}
