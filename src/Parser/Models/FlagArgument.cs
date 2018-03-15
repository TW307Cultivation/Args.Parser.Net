using System.Collections.Generic;
using System.Linq;
using Parser.Tests;

namespace Parser.Models
{
    class FlagArgument : OptionBase
    {
        public FlagArgument(string argument, HashSet<OptionBase> options)
        {
            if (!Config.FullArgRegex.Match(argument).Success &&
                !Config.AbbrArgRegex.Match(argument).Success)
            {
                throw new ParserException($"Invalid argument format: {argument}");
            }

            var argOption = argument.StartsWith(Config.FullArgPrefix) ?
                new FlagOption(argument.Substring(2), null, null) :
                new FlagOption(null, argument[1], null);

            var option = options.FirstOrDefault(e => e.Equals(argOption));

            if (option == null)
            {
                throw new ParserException($"Invalid argument format: {argument}");
            }

            this.Full = option.Full;
            this.Abbr = option.Abbr;
            this.Description = option.Description;
        }

        public override OptionType Type => OptionType.Flag;
    }
}
