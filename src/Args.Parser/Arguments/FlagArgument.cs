
using Args.Parser.Options;

namespace Args.Parser.Arguments
{
    class FlagArgument : ArgumentDefinition<bool>
    {
        internal FlagArgument(IOptionDefinitionMetadata option) : base(option)
        {
        }
    }
}
