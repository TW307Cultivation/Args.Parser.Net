using System.Collections.Generic;
using System.Linq;
using Args.Parser.Core;
using Args.Parser.Exceptions;

namespace Args.Parser.Models
{
    class FlagArgument : OptionBase
    {
        public FlagArgument(string argument, Command command)
        {
            var argOption = BuildFlagOption(argument);
            var option = command.GetOption(argOption);

            if (option == null)
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, argument);
            }

            Full = $"{Config.FullArgPrefix}{option.Full}";
            Abbr = $"{Config.AbbrArgPrefix}{option.Abbr}";
        }

        static FlagOption BuildFlagOption(string argument)
        {
            if (argument.StartsWith(Config.FullArgPrefix))
            {
                return new FlagOption(argument.Substring(2), null, null);
            }
            if (argument.StartsWith(Config.AbbrArgPrefix))
            {
                return new FlagOption(null, argument.Substring(1), null);
            }
            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, argument);
        }

        public override OptionType Type => OptionType.Flag;
    }
}
