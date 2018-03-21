using System;

namespace Args.Parser.Models
{
    class FlagOption : OptionBase
    {
        public FlagOption(string full, string abbr, string description) :
            base(full, abbr, description)
        {
            if (Full == null && Abbr == null)
            {
                throw new ArgumentException("The option need a full form or abbreviation form.");
            }

            if (Full != null && !Config.FullOptionRegex.Match(Full).Success)
            {
                throw new ArgumentException("This full form argument is invalid.");
            }

            if (Abbr != null && !Config.AbbrOptionRegex.Match(Abbr).Success)
            {
                throw new ArgumentException("This abbreviation form argument is invalid.");
            }
        }

        public override OptionType Type => OptionType.Flag;
    }
}
