using System.Text.RegularExpressions;

namespace Args.Parser.Options
{
    abstract class OptionDefinition : IOptionDefinitionMetadata
    {
        public abstract OptionType Type { get; }

        public IOptionSymbolMetadata SymbolMetadata { get; }

        public string Description { get; }

        static readonly Regex DescriptionRegex = new Regex(@"\s+", RegexOptions.Compiled);

        protected OptionDefinition(IOptionSymbolMetadata symbolMetadata, string description)
        {
            SymbolMetadata = symbolMetadata;
            Description = DescriptionRegex.Replace(description ?? string.Empty, " ");
        }
    }
}
