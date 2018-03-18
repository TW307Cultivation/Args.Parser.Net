using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Parser.Exceptions
{
    public class ArgsParsingException : Exception
    {
        public ArgsErrorCode Code { get; }

        public string Trigger { get; }

        public ArgsParsingException(ArgsErrorCode code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }

        public override string Message => messages[Code];

        readonly ReadOnlyDictionary<ArgsErrorCode, string> messages =
            new ReadOnlyDictionary<ArgsErrorCode, string>(
                new Dictionary<ArgsErrorCode, string>
                {
                    {ArgsErrorCode.InvalidArgument, "This argument is invalid."},
                    {ArgsErrorCode.EmptyOption, "The option need a full form or abbreviation form."},
                    {ArgsErrorCode.UndefinedOption, "Undefined option."},
                    {ArgsErrorCode.DuplicateOption, "Duplicate option."},
                });
    }
}
