using System.Collections.Generic;
using Args.Parser.Exceptions;
using Args.Parser.Models;

namespace Args.Parser
{
    /// <summary>
    /// Add option and create <see cref="ArgsParser"/>.
    /// </summary>
    public class ArgsParserBuilder
    {
        readonly HashSet<OptionBase> options = new HashSet<OptionBase>();

        /// <summary>
        /// Add flag option with full form, abbreviation form and description,
        /// full form and abbreviation form must specify one of them.
        /// </summary>
        /// <param name="fullForm">
        /// The option's full form.
        /// </param>
        /// <param name="abbrForm">
        /// The option's abbreviation form.
        /// </param>
        /// <param name="description">
        /// The option's description.
        /// </param>
        /// <returns>
        /// A <see cref="ArgsParserBuilder"/> that can add option continue.
        /// </returns>
        /// <exception cref="ArgsParsingException">
        /// Add duplicate option, or
        /// the <paramref name="fullForm"/> or <paramref name="abbrForm"/> is invalid.
        /// </exception>
        public ArgsParserBuilder AddFlagOption(string fullForm, char? abbrForm, string description)
        {
            if (options.Count > 0)
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.DuplicateOption, fullForm ?? abbrForm.ToString());
            }

            options.Add(new FlagOption(fullForm, abbrForm.ToString(), description));

            return this;
        }

        /// <summary>
        /// Build an <see cref="ArgsParser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ArgsParser"/> that holds the options added before.
        /// </returns>
        public ArgsParser Build()
        {
            return new ArgsParser(options);
        }
    }
}
