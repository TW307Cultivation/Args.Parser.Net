using System.Collections.Generic;
using System.Linq;
using Parser.Models;

namespace Parser
{
    public class ArgsParsingResult
    {
        public bool IsSuccess { get; set; }
        public ParsingError Error { get; set; }

        readonly HashSet<OptionBase> arguments;

        internal ArgsParsingResult(HashSet<OptionBase> arguments)
        {
            this.IsSuccess = true;
            this.arguments = arguments ?? new HashSet<OptionBase>();
        }

        internal ArgsParsingResult(ParsingError error)
        {
            this.IsSuccess = false;
            this.Error = error;
            this.arguments = new HashSet<OptionBase>();
        }

        public bool GetFlagValue(string flag)
        {
            var argument = new FlagArgument(flag?.ToLower(), flag?.ToLower());
            return arguments.Any(e => e.Equals(argument));
        }
    }

    public class ParsingError
    {
        public ParsingErrorCode Code { get; set; }
        public string Trigger { get; set; }
    }

    public enum ParsingErrorCode
    {
        InvalidArgument,
        UndefinedOption,
        DuplicatedOption
    }
}
