using System.Collections.Generic;
using Args.Parser.Commands;

namespace Args.Parser.Arguments
{
    class ArgumentsDefinition
    {
        internal ICommandDefinitionMetadata Command { get; }

        internal IList<FlagArgument> Flags { get; }

        internal ArgumentsDefinition(ICommandDefinitionMetadata command, List<FlagArgument> flags)
        {
            Command = command;
            Flags = flags ?? new List<FlagArgument>();
        }
    }
}
