using System;
using System.Collections.Generic;
using System.Linq;

namespace Args.Parser.Commands
{
    class CommandsDefinition
    {
        IList<ICommandDefinition> Commands { get; } = new List<ICommandDefinition>();

        public void RegisterCommand(ICommandDefinition command)
        {
            if (Commands.Any(e => e.Symbol == command.Symbol))
            {
                throw new InvalidOperationException("This command already exists.");
            }

            Commands.Add(command);
        }

        public IList<ICommandDefinition> GetCommands()
        {
            return new List<ICommandDefinition>(Commands);
        }
    }
}
