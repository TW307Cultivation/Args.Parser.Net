namespace Args.Parser.Options
{
    class FlagOptionDefinition : OptionDefinition
    {
        public override OptionType Type => OptionType.Flag;

        internal FlagOptionDefinition(IOptionSymbolMetadata symbolMetadata, string description) : base(symbolMetadata, description)
        {
        }
    }
}
