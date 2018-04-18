using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Arguments;
using Args.Parser.Commands;
using Args.Parser.Exceptions;

namespace Args.Parser.Core
{
    /// <summary>
    /// Argumnets Parser.
    /// </summary>
    public class ArgsParser
    {
        readonly ArgumentsBuilder argumentsBuilder;

        internal ArgsParser(CommandsDefinition commands)
        {
            argumentsBuilder = new ArgumentsBuilder(commands);
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
        public ArgsParsingResult Parse(IList<string> args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Any(e => e == null)) throw new ArgumentException(nameof(args));

            try
            {
                return new ArgsParsingResult(argumentsBuilder.Build(args));
            }
            catch (ParsingException e)
            {
                return new ArgsParsingResult(new ArgsParsingError(e.Code, e.Trigger));
            }
        }
    }
}
