using System.Linq;
using System.Text.RegularExpressions;
using Parser.Tests;

namespace Parser
{
    public class ArgsParser
    {
        public string FullForm { get; set; }
        public char? AbbreviationForm { get; set; }
        public string Description { get; set; }

        static readonly Regex FullFormRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);
        static readonly Regex AbbreviationFormRegex = new Regex(@"^-[a-zA-Z]{1}$", RegexOptions.Compiled);

        public ArgsParsingResult Parse(string[] arguments)
        {
            foreach (var argument in arguments)
            {
                if (FullFormRegex.Match(argument).Success || AbbreviationFormRegex.Match(argument).Success)
                {
                    continue;
                }
                throw new ParserException($"Invalid argument format: {argument}");
            }
            return null;
        }
    }
}
