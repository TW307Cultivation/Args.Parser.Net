using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;

namespace Args.Parser.Models
{
    class FlagArgument : OptionBase
    {
        public FlagArgument(string full, string abbr, string description = null) :
            base(full, abbr, description)
        {
        }

        public FlagArgument(string argument, IEnumerable<OptionBase> options)
        {
            var argOption = BuildFlagOption(argument);
            var option = options.FirstOrDefault(e => e.Equals(argOption));

            if (option == null)
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.UndefinedOption, argument);
            }

            Full = $"{Config.FullArgPrefix}{option.Full}";
            Abbr = $"{Config.AbbrArgPrefix}{option.Abbr}";
        }

        static FlagOption BuildFlagOption(string argument)
        {
            if (Config.FullArgRegex.Match(argument).Success)
            {
                return new FlagOption(argument.Substring(2), null, null);
            }

            if (Config.AbbrArgRegex.Match(argument).Success)
            {
                return new FlagOption(null, argument[1].ToString(), null);
            }

            throw new ArgsParsingException(ArgsParsingErrorCode.InvalidArgument, argument);
        }

        public override OptionType Type => OptionType.Flag;
    }
}
