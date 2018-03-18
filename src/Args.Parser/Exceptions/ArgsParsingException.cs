using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Args.Parser.Exceptions
{
    /// <summary>
    /// Exception during parsing.
    /// </summary>
    public class ArgsParsingException : Exception
    {
        /// <summary>
        /// The error type.
        /// </summary>
        public ArgsParsingErrorCode Code { get; }

        /// <summary>
        /// The argument that cause error when parse.
        /// </summary>
        public string Trigger { get; }

        /// <inheritdoc />
        /// <summary>
        /// Create <see cref="T:Args.Parser.Exceptions.ArgsParsingException" /> with
        /// <paramref name="code" /> and <paramref name="trigger" />
        /// </summary>
        /// <param name="code">
        /// The error type.
        /// </param>
        /// <param name="trigger">
        /// The argument that cause error when parse.
        /// </param>
        public ArgsParsingException(ArgsParsingErrorCode code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }

        /// <inheritdoc />
        /// <summary>
        /// The error message built with error type.
        /// </summary>
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
