using System.Collections.Generic;

namespace Args.Parser.Commands
{
    /// <summary>
    /// Command definition metadata.
    /// </summary>
    public interface ICommandDefinitionMetadata
    {
        /// <summary>
        /// The name of command.
        /// </summary>
        string Symbol { get; }

        /// <summary>
        /// The description of command.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Get all option definitions of command.
        /// </summary>
        /// <returns>
        /// <see cref="IOptionDefinitionMetadata"/>
        /// </returns>
        IEnumerable<IOptionDefinitionMetadata> GetRegisteredOptionsMetadata();
    }
}
