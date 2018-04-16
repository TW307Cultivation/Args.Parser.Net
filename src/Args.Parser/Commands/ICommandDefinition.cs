using System.Collections.Generic;
using Args.Parser.Options;

namespace Args.Parser.Commands
{
    interface ICommandDefinition : ICommandDefinitionMetadata
    {
        void RegisterOption(string fullForm, char? abbrForm, string description);

        IEnumerable<Option> GetOptions();
    }
}
