using System.Text.RegularExpressions;
using Parser.Tests;

namespace Parser
{
    public class ArgsParser
    {
        public string FullForm { get; set; }
        public char? AbbreviationForm { get; set; }
        public string Description { get; set; }

        readonly ArgsParsingResult result;

        static readonly Regex FullFormRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);
        static readonly Regex AbbreviationFormRegex = new Regex(@"^-[a-zA-Z]{1}$", RegexOptions.Compiled);

        public ArgsParser()
        {
            result = new ArgsParsingResult();
        }

        public ArgsParsingResult Parse(string[] arguments)
        {
            if (arguments == null)
            {
                return result;
            }

            foreach (var argument in arguments)
            {
                if (FullFormRegex.Match(argument).Success || AbbreviationFormRegex.Match(argument).Success)
                {
                    if (argument.Equals($"--{FullForm}") || argument.Equals($"-{AbbreviationForm}"))
                    {
                        if (FullForm != null)
                        {
                            result.AddFlag($"--{FullForm}");
                        }
                        if (AbbreviationForm != null)
                        {
                            result.AddFlag($"-{AbbreviationForm}");
                        }
                        continue;
                    }
                }
                throw new ParserException($"Invalid argument format: {argument}");
            }

            if (arguments.Length > 1)
            {
                throw new ParserException($"Duplicated arguments: {string.Join(" ", arguments)}");
            }

            return result;
        }
    }
}
