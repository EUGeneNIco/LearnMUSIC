using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class UnsupportedFileTypeException : Exception
    {
        public UnsupportedFileTypeException(string message)
            : base(message)
        {
        }
    }
}
