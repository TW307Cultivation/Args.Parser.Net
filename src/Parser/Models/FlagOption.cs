using Parser.Exceptions;

namespace Parser.Models
{
    class FlagOption : OptionBase
    {
        public FlagOption(string full, string abbr, string description) :
            base(full, abbr, description)
        {
            if (string.IsNullOrEmpty(Full) && string.IsNullOrEmpty(Abbr))
            {
                throw new ParserException("Must specify flag with full form or abbreviation form");
            }
            if (!string.IsNullOrEmpty(Full) && !Config.FullOptionRegex.Match(Full).Success)
            {
                throw new ParserException($"Invalid full form in this option: {this}");
            }
            if (!string.IsNullOrEmpty(Abbr) && !Config.AbbrOptionRegex.Match(Abbr).Success)
            {
                throw new ParserException($"Invalid abbreviation form in this option: {this}");
            }

            Full = Full?.ToLower();
            Abbr = Abbr?.ToLower();
        }

        public override OptionType Type => OptionType.Flag;
    }
}
