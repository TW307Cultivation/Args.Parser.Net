using System;
using Args.Parser.Core;

namespace Args.Parser.Commands
{
    /// <summary>
    /// Add options to command.
    /// </summary>
    public class CommandBuilder
    {
        readonly ArgsParserBuilder parserBuilder;

        readonly ICommandDefinition command;

        internal CommandBuilder(ArgsParserBuilder parserBuilder)
        {
            this.parserBuilder = parserBuilder;

            command = new DefaultCommand();
        }

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
        /// The option's description, default value is null.
        /// </param>
        /// <returns>
        /// A <see cref="ArgsParserBuilder"/> that can add option continuously.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Add duplicate option, or
        /// the <paramref name="fullForm"/> or <paramref name="abbrForm"/> is invalid.
        /// </exception>
        public CommandBuilder AddFlagOption(string fullForm, char? abbrForm, string description = null)
        {
            command.RegisterOption(fullForm, abbrForm, description);
            return this;
        }

        /// <summary>
        /// End defining command and add the command to <see cref="ArgsParserBuilder"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ArgsParserBuilder"/> that holds the commands added before.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Add duplicate command.
        /// </exception>
        public ArgsParserBuilder EndCommand()
        {
            parserBuilder.Commands.RegisterCommand(command);
            return parserBuilder;
        }
    }
}
