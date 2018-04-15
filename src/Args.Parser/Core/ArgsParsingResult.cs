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
        /// Get details of paring error.
        /// </summary>
        public ArgsParsingError Error { get; }

        readonly HashSet<OptionBase> arguments;
        readonly Command command;

        internal ArgsParsingResult(HashSet<OptionBase> arguments, Command command)
        {
            this.IsSuccess = true;
            this.arguments = arguments ?? new HashSet<OptionBase>();
            this.command = command;
        }

        internal ArgsParsingResult(ArgsParsingError error)
        {
            this.IsSuccess = false;
            this.Error = error;
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
                var argument = new FlagArgument(flag, command);
                return arguments.Any(e => e.Equals(argument));
            }
            catch (ArgsParsingException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
