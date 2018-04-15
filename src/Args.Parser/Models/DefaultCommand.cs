using System.Collections.Generic;
using System.Linq;
using Args.Parser.Core;

namespace Args.Parser.Models
{
    class DefaultCommand : ICommandDefinitionMetadata
    {
        readonly HashSet<OptionBase> options;

        public DefaultCommand(HashSet<OptionBase> options)
        {
            this.options = options ?? new HashSet<OptionBase>();
        }

        public OptionBase GetOption(FlagOption argOption)
        {
            return options.FirstOrDefault(e => e.Equals(argOption));
        }

        public string Symbol => null;
    }
}
