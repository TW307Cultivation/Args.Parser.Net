using System.Collections.Generic;
using Parser.Exceptions;
using Parser.Models;

namespace Parser
{
    public class ArgsParser
    {
        readonly HashSet<OptionBase> arguments = new HashSet<OptionBase>();
        readonly HashSet<OptionBase> options;

        internal ArgsParser(HashSet<OptionBase> options)
        {
            this.options = options ?? new HashSet<OptionBase>();
        }

        public ArgsParsingResult Parse(string[] args)
        {
            if (args == null)
            {
                return new ArgsParsingResult(arguments);
            }

            foreach (var arg in args)
            {
                var argument = new FlagArgument(arg, options);
                if (arguments.Contains(argument))
                {
                    throw new ParserException($"Duplicated arguments: {string.Join(" ", args)}");
                }

                arguments.Add(argument);
            }

            return new ArgsParsingResult(arguments);
        }
    }
}