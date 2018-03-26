using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Args.Parser.Core;

namespace Args.Parser.Exceptions
{
    class ArgsParsingException : Exception
    {
        public ArgsParsingErrorCode Code { get; }

        public string Trigger { get; }

        public ArgsParsingException(ArgsParsingErrorCode code, string trigger)
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
