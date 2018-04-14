using System.Collections.Generic;

namespace Args.Parser.Models
{
    class FlagOption2 : IOption
    {
        public FlagOption2(string full, char? abbr, string description)
        {
            Full = full;
            Abbr = abbr;
            Description = description;
        }

        public OptionType Type => OptionType.Flag;

        public string Full { get; }

        public char? Abbr { get; }

        public string Description { get; }
    }
}
