using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Models;

namespace Args.Parser.Commands
{
    class DefaultCommand : ICommandDefinition
    {
        public string Symbol => null;

        IList<OptionBase> Options { get; } = new List<OptionBase>();

        public void RegisterOption(string fullForm, char? abbrForm, string description)
        {
            var option = new FlagOption(fullForm, abbrForm?.ToString(), description);

            if (Options.Any(e => e.Equals(option)))
            {
                throw new ArgumentException("Duplicate option.");
            }
            Options.Add(option);
        }

        public OptionBase GetOption(FlagOption argOption)
        {
            return Options.FirstOrDefault(e => e.Equals(argOption));
        }

    }
}
