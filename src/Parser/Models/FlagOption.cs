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
                throw new ArgsParsingException(ArgsErrorCode.EmptyOption, null);
            }

            if (!string.IsNullOrEmpty(Full) && !Config.FullOptionRegex.Match(Full).Success)
            {
                throw new ArgsParsingException(ArgsErrorCode.InvalidArgument, Full);
            }

            if (!string.IsNullOrEmpty(Abbr) && !Config.AbbrOptionRegex.Match(Abbr).Success)
            {
                throw new ArgsParsingException(ArgsErrorCode.InvalidArgument, Abbr);
            }
        }

        public override OptionType Type => OptionType.Flag;
    }
}
