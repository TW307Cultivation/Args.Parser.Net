namespace Args.Parser.Commands
{
    /// <summary>
    /// The option definition.
    /// </summary>
    public interface IOptionDefinitionMetadata
    {
        /// <summary>
        /// The symbol metadata of option.
        /// </summary>
        IOptionSymbolMetadata SymbolMetadata { get; }

        /// <summary>
        /// The description of option.
        /// </summary>
        string Description { get; }
    }
}
