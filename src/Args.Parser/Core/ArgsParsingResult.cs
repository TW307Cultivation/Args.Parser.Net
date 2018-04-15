using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;
using Args.Parser.Models;

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
        public ICommandDefinitionMetadata Command { get; }

        /// <summary>
        /// Get details of paring error.
        /// </summary>
        public ArgsParsingError Error { get; }

        readonly HashSet<OptionBase> arguments;

        internal ArgsParsingResult(HashSet<OptionBase> arguments, ICommandDefinitionMetadata command)
        {
            IsSuccess = true;
            Command = command;

            this.arguments = arguments ?? new HashSet<OptionBase>();
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
            if (flag == null) throw new ArgumentNullException(nameof(flag));
            if (!IsSuccess) throw new InvalidOperationException();

            try
            {
                var argument = new FlagArgument(flag, (DefaultCommand) Command);
                return arguments.Any(e => e.Equals(argument));
            }
            catch (ArgsParsingException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
