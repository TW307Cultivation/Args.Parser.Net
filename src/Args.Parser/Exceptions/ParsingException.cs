using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Args.Parser.Core;

namespace Args.Parser.Exceptions
{
    class ParsingException : Exception
    {
        internal ArgsParsingErrorCode Code { get; }

        internal string Trigger { get; }

        internal ParsingException(ArgsParsingErrorCode code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }

        public override string Message => messages[Code];

        readonly ReadOnlyDictionary<ArgsParsingErrorCode, string> messages =
            new ReadOnlyDictionary<ArgsParsingErrorCode, string>(
                new Dictionary<ArgsParsingErrorCode, string>
                {
                    {ArgsParsingErrorCode.FreeValueNotSupported, "Free value not support."},
                    {ArgsParsingErrorCode.DuplicateFlagsInArgs, "Duplicate option."},
                });
    }
}
