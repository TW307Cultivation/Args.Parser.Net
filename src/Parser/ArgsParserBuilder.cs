using System.Text.RegularExpressions;
using Parser.Tests;

namespace Parser
{
    public class ArgsParserBuilder
    {
        bool initialized;
        readonly ArgsParser parser;
        static readonly Regex FullFormRegex = new Regex(@"^[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        public ArgsParserBuilder()
        {
            initialized = false;
            parser = new ArgsParser();
        }

        public ArgsParserBuilder AddFlagOption(char abbreviationForm)
        {
            return AddFlagOption(null, abbreviationForm, null);
        }

        public ArgsParserBuilder AddFlagOption(string fullForm, char abbreviationForm)
        {
            return AddFlagOption(fullForm, abbreviationForm, null);
        }

        public ArgsParserBuilder AddFlagOption(string fullForm, char? abbreviationForm = null, string description = null)
        {
            if (initialized)
            {
                throw new ParserException("Can only specify flag once.");
            }

            if (string.IsNullOrWhiteSpace(fullForm) && string.IsNullOrWhiteSpace(abbreviationForm?.ToString()))
            {
                throw new ParserException("Must specify flag with full form or abbreviation form.");
            }

            if (fullForm != null && !FullFormRegex.Match(fullForm).Success)
            {
                throw new ParserException("Invalid full form.");
            }

            if (abbreviationForm != null && !char.IsLetter(abbreviationForm.Value))
            {
                throw new ParserException("Invalid abbreviation form.");
            }

            initialized = true;
            parser.FullForm = fullForm;
            parser.AbbreviationForm = abbreviationForm;
            parser.Description = description;

            return this;
        }

        public ArgsParser Build()
        {
            return parser;
        }
    }
}
