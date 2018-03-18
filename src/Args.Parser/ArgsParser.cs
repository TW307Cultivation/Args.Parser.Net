using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;
using Args.Parser.Models;

namespace Args.Parser
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
                foreach (var arg in args ?? new string[] { })
                {
                    var argument = new FlagArgument(arg, options);

                    if (arguments.Any(e => e.Equals(argument)))
                    {
                        throw new ArgsParsingException(ArgsParsingErrorCode.DuplicateOption, arg);
                    }

                    arguments.Add(argument);
                }

                return new ArgsParsingResult(arguments);
            }
            catch (ArgsParsingException e)
            {
                return new ArgsParsingResult(new ArgsParsingError
                {
                    Code = e.Code,
                    Trigger = e.Trigger
                });
            }
        }
    }
}
