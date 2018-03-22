﻿using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;
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
        readonly HashSet<OptionBase> options;

        internal ArgsParsingResult(HashSet<OptionBase> arguments, HashSet<OptionBase> options)
        {
            this.IsSuccess = true;
            this.arguments = arguments ?? new HashSet<OptionBase>();
            this.options = options ?? new HashSet<OptionBase>();
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
                var argument = new FlagArgument(flag, options);
                return arguments.Any(e => e.Equals(argument));
            }
            catch (ArgsParsingException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
