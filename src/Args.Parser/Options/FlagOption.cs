using Args.Parser.Commands;

namespace Args.Parser.Options
{
    class FlagOption : Option
    {
        public override OptionType Type => OptionType.Flag;

        public FlagOption(IOptionSymbolMetadata symbolMetadata, string description) : base(symbolMetadata, description)
        {
        }
    }
}
