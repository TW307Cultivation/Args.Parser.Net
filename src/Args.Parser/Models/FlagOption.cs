using Args.Parser.Exceptions;

namespace Args.Parser.Models
{
    class FlagOption : OptionBase
    {
        public FlagOption(string full, string abbr, string description) :
            base(full, abbr, description)
        {
            if (string.IsNullOrEmpty(Full) && string.IsNullOrEmpty(Abbr))
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.EmptyOption, null);
            }

            if (!string.IsNullOrEmpty(Full) && !Config.FullOptionRegex.Match(Full).Success)
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.InvalidArgument, Full);
            }

            if (!string.IsNullOrEmpty(Abbr) && !Config.AbbrOptionRegex.Match(Abbr).Success)
            {
                throw new ArgsParsingException(ArgsParsingErrorCode.InvalidArgument, Abbr);
            }
        }

        public override OptionType Type => OptionType.Flag;
    }
}
