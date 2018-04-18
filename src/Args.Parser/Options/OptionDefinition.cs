namespace Args.Parser.Options
{
    abstract class OptionDefinition : IOptionDefinitionMetadata
    {
        public abstract OptionType Type { get; }

        public IOptionSymbolMetadata SymbolMetadata { get; }

        public string Description { get; }

        protected OptionDefinition(IOptionSymbolMetadata symbolMetadata, string description)
        {
            SymbolMetadata = symbolMetadata;
            Description = description ?? string.Empty;
        }
    }
}
