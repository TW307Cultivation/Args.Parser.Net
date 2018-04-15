using System;
using System.Collections.Generic;
using Args.Parser.Models;

namespace Args.Parser.Core
{
    /// <summary>
    /// Define commands and create <see cref="ArgsParser"/>.
    /// </summary>
    public class ArgsParserBuilder
    {
        Command command;

        /// <summary>
        /// Begin defining default command.
        /// </summary>
        /// <returns>
        /// A <see cref="CommandBuilder"/> to add flags.
        /// </returns>
        public CommandBuilder BeginDefaultCommand()
        {
            return new CommandBuilder(this);
        }

        /// <summary>
        /// Build an <see cref="ArgsParser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ArgsParser"/> that holds the options added before.
        /// </returns>
        public ArgsParser Build()
        {
            return new ArgsParser(command);
        }

        internal ArgsParserBuilder RegisterCommand(Command command)
        {
            if (this.command != null) throw new InvalidOperationException();

            this.command = command;

            return this;
        }
    }
}
