using Parser.Exceptions;

namespace Parser.Models
{
    class FlagOption : OptionBase
    {
        public FlagOption(string full, char? abbr, string description)
        {
            if (string.IsNullOrWhiteSpace(full) && string.IsNullOrWhiteSpace(abbr?.ToString()))
            {
                throw new ParserException("Must specify flag with full form or abbreviation form.");
            }

            if (full != null && !Config.FullOptionRegex.Match(full).Success)
            {
                throw new ParserException("Invalid full form.");
            }

            if (abbr != null && !Config.AbbrOptionRegex.Match(abbr.ToString()).Success)
            {
                throw new ParserException("Invalid abbreviation form.");
            }

            this.Full = full?.ToLower();
            this.Abbr = abbr?.ToString().ToLower()[0];
            this.Description = description;
        }

        public override OptionType Type => OptionType.Flag;
    }
}
