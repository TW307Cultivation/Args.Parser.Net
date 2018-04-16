using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Commands;
using Args.Parser.Exceptions;
using Args.Parser.Options;

namespace Args.Parser.Core
{
    /// <summary>
    /// Parsing result.
    /// </summary>
    public class ArgsParsingResult
    {
        /// <summary>
        /// Represents the parsing result whether successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Get the definition metadata of <see cref="Command"/>.
        /// </summary>
        public ICommandDefinitionMetadata Command => command;

        /// <summary>
        /// Get details of paring error.
        /// </summary>
        public ArgsParsingError Error { get; }

        readonly IList<Option> arguments;
        readonly ICommandDefinition command;

        internal ArgsParsingResult(IList<Option> arguments, ICommandDefinition command)
        {
            IsSuccess = true;

            this.command = command;
            this.arguments = arguments ?? new List<Option>();
        }

        internal ArgsParsingResult(ArgsParsingError error)
        {
            IsSuccess = false;
            Error = error;
        }

        /// <summary>
        /// Get flag value.
        /// </summary>
        /// <param name="flag">
        /// The full form or abbreviation form flag.
        /// </param>
        /// <returns>
        /// If the flag is specified, return true, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The flag argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The flag argument is invalid or undefined.
        /// </exception>
        public bool GetFlagValue(string flag)
        {
            if (!IsSuccess) throw new InvalidOperationException();

            try
            {
                var symbol = new OptionSymbol(flag);

                if (!command.GetOptions().Any(c => c.Symbol.Equals(symbol)))
                {
                    throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, flag);
                }

                return arguments.Any(e => e.Symbol.Equals(symbol));
            }
            catch (ArgsParsingException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
