using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Options;

namespace Args.Parser.Commands
{
    class DefaultCommand : ICommandDefinition
    {
        IList<Option> Options { get; } = new List<Option>();

        public string Symbol => null;

        public void RegisterOption(string fullForm, char? abbrForm, string description)
        {
            var symbol = new OptionSymbol(fullForm, abbrForm);

            if (Options.Any(e => e.Symbol.Equals(symbol)))
            {
                throw new ArgumentException("Duplicate option.");
            }

            Options.Add(new FlagOption(symbol, description));
        }

        public IEnumerable<Option> GetOptions()
        {
            return new List<Option>(Options);
        }

        public Option GetOption(FlagOption argOption)
        {
            return Options.FirstOrDefault(e => e.Symbol.Equals(argOption.Symbol));
        }
    }
}
