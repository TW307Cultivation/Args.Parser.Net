using Parser.Exceptions;

namespace Parser.Models
{
    class FlagOption : OptionBase
    {
        public FlagOption(string full, char? abbr, string description) : base(full, abbr, description)
        {
            if (string.IsNullOrWhiteSpace(full) && string.IsNullOrWhiteSpace(abbr?.ToString()))
            {
                throw new ParserException("Must specify flag with full form or abbreviation form");
            }

            if (full != null && !Config.FullOptionRegex.Match(full).Success)
            {
                throw new ParserException($"Invalid full form in this option: {this}");
            }

            if (abbr != null && !Config.AbbrOptionRegex.Match(abbr.ToString()).Success)
            {
                throw new ParserException($"Invalid abbreviation form in this option: {this}");
            }

            Full = full?.ToLower();
            Abbr = abbr?.ToString().ToLower()[0];
        }

        public override OptionType Type => OptionType.Flag;
    }
}
