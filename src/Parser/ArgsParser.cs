using System;
using System.Collections.Generic;
using System.Linq;
using Parser.Exceptions;
using Parser.Models;

namespace Parser
{
    /// <summary>
    /// Argumnets Parser.
    /// </summary>
    public class ArgsParser
    {
        readonly HashSet<OptionBase> arguments = new HashSet<OptionBase>();
        readonly HashSet<OptionBase> options;

        internal ArgsParser(HashSet<OptionBase> options)
        {
            this.options = options ?? new HashSet<OptionBase>();
        }

        /// <summary>
        /// Parse arguments accordinating to options.
        /// </summary>
        /// <param name="args">
        /// Input arguments, full form with '--' prefix, and abbreviation form with '-' prefix.
        /// </param>
        /// <returns>
        /// A parsing result <see cref="ArgsParsingResult"/>.
        /// </returns>
        public ArgsParsingResult Parse(string[] args)
        {
            try
            {
                foreach (var arg in args ?? new string[]{})
                {
                    var argument = new FlagArgument(arg, options);

                    if (arguments.Any(e => e.Equals(argument)))
                    {
                        throw new ParsingException(ParsingErrorCode.DuplicatedOption, arg);
                    }

                    arguments.Add(argument);
                }

                return new ArgsParsingResult(arguments);
            }
            catch (ParsingException e)
            {
                return new ArgsParsingResult(new ParsingError
                {
                    Code = e.Code,
                    Trigger = e.Trigger
                });
            }
        }
    }
}
