using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;
using Args.Parser.Models;

namespace Args.Parser.Core
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
        /// Input arguments, full form or abbreviation form.
        /// </param>
        /// <returns>
        /// A parsing result <see cref="ArgsParsingResult"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The args is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The args has null value.
        /// </exception>
        public ArgsParsingResult Parse(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if (args.Any(e => e == null))
            {
                throw new ArgumentException(nameof(args));
            }

            try
            {
                foreach (var arg in args)
                {
                    var argument = new FlagArgument(arg, options);
                    if (arguments.Any(e => e.Equals(argument)))
                    {
                        throw new ArgsParsingException(ArgsParsingErrorCode.DuplicateFlagsInArgs, arg);
                    }

                    arguments.Add(argument);
                }

                return new ArgsParsingResult(arguments, options);
            }
            catch (ArgsParsingException e)
            {
                return new ArgsParsingResult(new ArgsParsingError(e.Code, e.Trigger));
            }
        }
    }
}
