namespace Args.Parser.Commands
{
    /// <summary>
    /// The option symbol metadata.
    /// </summary>
    public interface IOptionSymbolMetadata
    {
        /// <summary>
        /// The full form of symbol.
        /// </summary>
        string FullForm { get; }

        /// <summary>
        /// The abbreviation form of symbol.
        /// </summary>
        char? Abbreviation { get; }
    }
}
