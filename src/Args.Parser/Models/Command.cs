using System.Collections.Generic;
using System.Linq;

namespace Args.Parser.Models
{
    class Command
    {
        readonly HashSet<OptionBase> options;

        public Command(HashSet<OptionBase> options)
        {
            this.options = options ?? new HashSet<OptionBase>();
        }

        public OptionBase GetOption(FlagOption argOption)
        {
            return options.FirstOrDefault(e => e.Equals(argOption));
        }
    }
}
