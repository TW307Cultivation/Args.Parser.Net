using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Models;

namespace Args.Parser
{
    /// <summary>
    /// Parsing result.
    /// </summary>
    public class ArgsParsingResult
    {
        /// <summary>
        /// Represents the parsing result whether successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Get details of paring error.
        /// </summary>
        public ArgsParsingError Error { get; set; }

        readonly HashSet<OptionBase> arguments;

        internal ArgsParsingResult(HashSet<OptionBase> arguments)
        {
            this.IsSuccess = true;
            this.arguments = arguments ?? new HashSet<OptionBase>();
        }

        internal ArgsParsingResult(ArgsParsingError error)
        {
            this.IsSuccess = false;
            this.Error = error;
            this.arguments = new HashSet<OptionBase>();
        }

        /// <summary>
        /// Get flag value.
        /// </summary>
        /// <param name="flag">
        /// The full form or abbreviation form with prefix of the flag.
        /// </param>
        /// <returns>
        /// If the flag is specified, return true, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The flag argument is null.
        /// </exception>
        public bool GetFlagValue(string flag)
        {
            if (flag == null) throw new ArgumentNullException(nameof(flag));

            var argument = new FlagArgument(flag, flag);
            return arguments.Any(e => e.Equals(argument));
        }
    }
}
