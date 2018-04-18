using System;
using System.Linq;
using Args.Parser.Arguments;
using Args.Parser.Commands;
using Args.Parser.Exceptions;
using Args.Parser.Options;

namespace Args.Parser.Core
{
    /// <summary>
    /// Parsing result.
    /// </summary>
    public class ArgsParsingResult
    {
        /// <summary>
        /// Represents the parsing result whether successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Get the definition metadata of <see cref="Command"/>.
        /// </summary>
        public ICommandDefinitionMetadata Command => definition?.Command;

        /// <summary>
        /// Get details of paring error.
        /// </summary>
        public ArgsParsingError Error { get; }

        readonly ArgumentsDefinition definition;

        internal ArgsParsingResult(ArgsParsingError error)
        {
            IsSuccess = false;
            Error = error;
        }

        internal ArgsParsingResult(ArgumentsDefinition definition)
        {
            IsSuccess = true;

            this.definition = definition;
        }

        /// <summary>
        /// Get flag value.
        /// </summary>
        /// <param name="flag">
        /// The full form or abbreviation form flag.
        /// </param>
        /// <returns>
        /// If the flag is specified, return true, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The flag argument is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The flag argument is invalid or undefined.
        /// </exception>
        public bool GetFlagValue(string flag)
        {
            if (!IsSuccess) throw new InvalidOperationException();

            try
            {
                var symbol = new OptionSymbol(flag);
                var option = definition.Flags.FirstOrDefault(c => c.Option.SymbolMetadata.Equals(symbol));

                if (option == null)
                {
                    throw new ParsingException(ArgsParsingErrorCode.FreeValueNotSupported, flag);
                }

                return option.Value;
            }
            catch (ParsingException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
