using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class FileNotSavedException : Exception
    {
        public FileNotSavedException(string message)
            : base(message)
        {
        }
    }
}
