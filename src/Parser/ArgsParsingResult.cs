using System.Collections.Generic;

namespace Parser
{
    public class ArgsParsingResult
    {
        public bool IsSuccess { get; set; }
        readonly HashSet<string> flags;

        public ArgsParsingResult()
        {
            IsSuccess = true;
            flags = new HashSet<string>();
        }

        public void AddFlag(string argument)
        {
            flags.Add(argument);
        }

        public bool GetFlagValue(string argument)
        {
            return flags.Contains(argument);
        }
    }
}
