using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Args.Parser.Commands;
using Args.Parser.Core;
using Args.Parser.Exceptions;
using Args.Parser.Options;

namespace Args.Parser.Arguments
{
    class ArgumentsBuilder
    {
        static readonly Regex FullArgRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);
        static readonly Regex AbbrArgRegex = new Regex(@"^-[a-zA-Z]+$", RegexOptions.Compiled);

        readonly CommandsDefinition commands;

        ICommandDefinition command;
        List<FlagArgument> flags;

        internal ArgumentsBuilder(CommandsDefinition commands)
        {
            this.commands = commands;
        }

        internal ArgumentsDefinition Build(IList<string> args)
        {
            SetCommand();
            SetFlagArguments(args);

            return new ArgumentsDefinition(command, flags);
        }

        void SetCommand()
        {
            command = commands.GetCommand();
        }

        void SetFlagArguments(IList<string> args)
        {
            flags = InitArguments(OptionType.Flag);

            args.ToList().ForEach(arg =>
            {
                TransferSymbols(arg).ForEach(e =>
                {
                    var flag = flags.FirstOrDefault(c => c.Option.SymbolMetadata.Equals(e));

                    if (flag == null) throw new ParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
                    if (flag.Value) throw new ParsingException(ArgsParsingErrorCode.DuplicateFlagsInArgs, arg);

                    flag.SetValue(true);
                });
            });
        }

        List<FlagArgument> InitArguments(OptionType type)
        {
            return command.GetRegisteredOptionsMetadata()
                .Cast<OptionDefinition>()
                .Where(e => e.Type == type)
                .Select(e => new FlagArgument(e))
                .ToList();
        }

        static List<OptionSymbol> TransferSymbols(string arg)
        {
            if (FullArgRegex.Match(arg).Success)
            {
                return new List<OptionSymbol>() {new OptionSymbol(arg.Substring(2), null)};
            }
            if (AbbrArgRegex.Match(arg).Success)
            {
                return arg.Substring(1).Select(e => new OptionSymbol(null, e)).ToList();
            }
            throw new ParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
        }
    }
}
