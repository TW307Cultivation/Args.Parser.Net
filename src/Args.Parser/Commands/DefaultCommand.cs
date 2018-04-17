using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Options;

namespace Args.Parser.Commands
{
    class DefaultCommand : ICommandDefinition
    {
        IList<IOptionDefinitionMetadata> Options { get; } = new List<IOptionDefinitionMetadata>();

        public string Symbol => null;

        public string Description => string.Empty;

        public IEnumerable<IOptionDefinitionMetadata> GetRegisteredOptionsMetadata()
        {
            return new List<IOptionDefinitionMetadata>(Options);
        }

        public void RegisterOption(string fullForm, char? abbrForm, string description)
        {
            var symbol = new OptionSymbol(fullForm, abbrForm);

            if (Options.Any(e => e.SymbolMetadata.Equals(symbol)))
            {
                throw new ArgumentException("Duplicate option.");
            }

            Options.Add(new FlagOption(symbol, description));
        }
    }
}
