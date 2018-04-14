namespace Args.Parser.Models
{
    interface IOption
    {
        OptionType Type { get; }

        string Full { get; }

        char? Abbr { get; }

        string Description { get; }
    }
}
