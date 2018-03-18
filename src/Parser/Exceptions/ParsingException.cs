using System;

namespace Parser.Exceptions
{
    class ParsingException : Exception
    {
        public ParsingErrorCode Code { get; }

        public string Trigger { get; }

        public ParsingException(ParsingErrorCode code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }
    }
}
