using Args.Parser.Commands;

namespace Args.Parser.Options
{
    abstract class Option : IOptionDefinitionMetadata
    {
        public abstract OptionType Type { get; }

        public IOptionSymbolMetadata SymbolMetadata { get; }

        public string Description { get; }

        protected Option(IOptionSymbolMetadata symbolMetadata, string description)
        {
            SymbolMetadata = symbolMetadata;
            Description = description ?? string.Empty;
        }
    }
}
