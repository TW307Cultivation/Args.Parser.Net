using System.Collections.Generic;
using System.Linq;
using Parser.Models;

namespace Parser
{
    public class ArgsParsingResult
    {
        public bool IsSuccess { get; set; }
        readonly HashSet<OptionBase> arguments;

        internal ArgsParsingResult(HashSet<OptionBase> arguments)
        {
            this.IsSuccess = true;
            this.arguments = arguments ?? new HashSet<OptionBase>();
        }

        public bool GetFlagValue(string flag)
        {
            var argument = new FlagArgument(flag?.ToLower(), flag?.ToLower());
            return arguments.Any(e => e.Equals(argument));
        }
    }
}
