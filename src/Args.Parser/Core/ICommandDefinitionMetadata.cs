using Args.Parser.Models;

namespace Args.Parser.Core
{
    /// <summary>
    /// Command definition metadata.
    /// </summary>
    public interface ICommandDefinitionMetadata
    {
        /// <summary>
        /// The name of <see cref="DefaultCommand"/>.
        /// </summary>
        string Symbol { get; }
    }
}
