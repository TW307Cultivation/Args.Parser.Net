using System;

namespace Parser.Tests
{
    public class FlagException : Exception
    {
        public FlagException()
        {
        }

        public FlagException(string message) : base(message)
        {
        }
    }
}
