namespace Args.Parser.Options
{
    abstract class Option
    {
        public abstract OptionType Type { get; }

        public OptionSymbol Symbol { get; }

        public string Description { get; }

        protected Option(OptionSymbol symbol, string description)
        {
            Symbol = symbol;
            Description = description;
        }
    }
}
