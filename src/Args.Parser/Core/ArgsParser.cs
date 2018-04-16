using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Args.Parser.Commands;
using Args.Parser.Exceptions;
using Args.Parser.Options;

namespace Args.Parser.Core
{
    /// <summary>
    /// Argumnets Parser.
    /// </summary>
    public class ArgsParser
    {
        static readonly Regex FullArgRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        static readonly Regex AbbrArgRegex = new Regex(@"^-[a-zA-Z]+$", RegexOptions.Compiled);

        readonly IList<Option> arguments = new List<Option>();

        readonly CommandsDefinition commands;

        internal ArgsParser(CommandsDefinition commands)
        {
            this.commands = commands;
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

            var command = commands.GetCommand(null);
            try
            {
                foreach (var arg in args)
                {
                    BuildOptions(arg).ForEach(e =>
                    {
                        var option = command.GetOptions().FirstOrDefault(c => c.Symbol.Equals(e));
                        if (option == null)
                        {
                            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
                        }
                        if (arguments.Any(a => a.Symbol.Equals(e)))
                        {
                            throw new ArgsParsingException(ArgsParsingErrorCode.DuplicateFlagsInArgs, arg);
                        }
                        arguments.Add(option);
                    });
                }

                return new ArgsParsingResult(arguments, command);
            }
            catch (ArgsParsingException e)
            {
                return new ArgsParsingResult(new ArgsParsingError(e.Code, e.Trigger));
            }
        }

        List<OptionSymbol> BuildOptions(string arg)
        {
            if (FullArgRegex.Match(arg).Success)
            {
                return new List<OptionSymbol>()
                {
                    new OptionSymbol(arg.Substring(2), null)
                };
            }
            if (AbbrArgRegex.Match(arg).Success)
            {
                try
                {
                    return arg.Substring(1).Select(e => new OptionSymbol(null, e)).ToList();
                }
                catch (ArgsParsingException e)
                {
                    throw new ArgsParsingException(e.Code, arg);
                }
            }
            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
        }
    }
}
