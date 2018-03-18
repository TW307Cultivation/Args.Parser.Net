using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Args.Parser.Exceptions
{
    public class ArgsParsingException : Exception
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
                    {ArgsParsingErrorCode.InvalidArgument, "This argument is invalid."},
                    {ArgsParsingErrorCode.EmptyOption, "The option need a full form or abbreviation form."},
                    {ArgsParsingErrorCode.UndefinedOption, "Undefined option."},
                    {ArgsParsingErrorCode.DuplicateOption, "Duplicate option."},
                });
    }
}
