namespace Args.Parser.Options
{
    class FlagOption : Option
    {
        public override OptionType Type => OptionType.Flag;

        public FlagOption(OptionSymbol symbol, string description) : base(symbol, description)
        {
        }
    }
}
