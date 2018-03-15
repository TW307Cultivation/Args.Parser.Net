using System;
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
            return arguments
                .Where(e => e.Type == OptionType.Flag)
                .Any(e => FlagEquals(e, flag));
        }

        bool FlagEquals(OptionBase arg, string y)
        {
            return string.Equals($"{Config.FullArgPrefix}{arg.Full}", y, StringComparison.OrdinalIgnoreCase) ||
                string.Equals($"{Config.AbbrArgPrefix}{arg.Abbr}", y, StringComparison.OrdinalIgnoreCase);
        }
    }
}
