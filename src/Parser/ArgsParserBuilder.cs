using System.Collections.Generic;
using Parser.Models;
using Parser.Tests;

namespace Parser
{
    public class ArgsParserBuilder
    {
        readonly HashSet<OptionBase> options = new HashSet<OptionBase>();

        public ArgsParserBuilder AddFlagOption(char abbrForm)
        {
            return AddFlagOption(null, abbrForm, null);
        }

        public ArgsParserBuilder AddFlagOption(string fullForm, char abbrForm)
        {
            return AddFlagOption(fullForm, abbrForm, null);
        }

        public ArgsParserBuilder AddFlagOption(string fullForm, char? abbForm = null, string description = null)
        {
            if (options.Count > 0)
            {
                throw new ParserException("Can only specify flag once.");
            }

            options.Add(new FlagOption(fullForm, abbForm, description));

            return this;
        }

        public ArgsParser Build()
        {
            return new ArgsParser(options);
        }
    }
}
