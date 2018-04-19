using System.Linq;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParsingResultTests
{
    public class when_get_command_definition
    {
        readonly ArgsParser parser;

        public when_get_command_definition()
        {
            parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", 'r')
                .EndCommand()
                .Build();
        }

        [Fact]
        void should_return_null_when_parse_failed()
        {
            var result = parser.Parse(new[] {"what_is_this"});

            Assert.False(result.IsSuccess);
            Assert.Null(result.Command);
        }

        [Fact]
        void should_get_command_definition()
        {
            var result = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", 'r')
                .AddFlagOption("force", 'f')
                .EndCommand()
                .Build()
                .Parse(new[] {"--recurse"});

            Assert.True(result.IsSuccess);
            Assert.Null(result.Command.Symbol);
            Assert.Empty(result.Command.Description);
            Assert.Equal(2, result.Command.GetRegisteredOptionsMetadata().Count());
        }

        [Theory]
        [InlineData("recurse", "recurse")]
        [InlineData(null, null)]
        void should_get_option_symbol_full_form(string full, string expected)
        {
            var result = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption(full, 'r')
                .EndCommand()
                .Build()
                .Parse(new string[] { });

            var option = result.Command
                .GetRegisteredOptionsMetadata()
                .Single(e => e.SymbolMetadata.Abbreviation == 'r');

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, option.SymbolMetadata.FullForm);
        }

        [Theory]
        [InlineData('r', 'r')]
        [InlineData(null, null)]
        void should_get_option_symbol_abbr_form(char? abbr, char? expected)
        {
            var result = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", abbr)
                .EndCommand()
                .Build()
                .Parse(new string[] { });

            var option = result.Command
                .GetRegisteredOptionsMetadata()
                .Single(e => e.SymbolMetadata.FullForm == "recurse");

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, option.SymbolMetadata.Abbreviation);
        }

        [Theory]
        [InlineData("description", "description")]
        [InlineData(null, "")]
        [InlineData("a\r\nb\rc\nd", "a b c d")]
        void should_get_option_description(string description, string expected)
        {
            var result = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", 'r', description)
                .EndCommand()
                .Build()
                .Parse(new string[] { });

            var option = result.Command
                .GetRegisteredOptionsMetadata()
                .Single(e => e.SymbolMetadata.FullForm == "recurse");

            Assert.True(result.IsSuccess);
            Assert.Equal(expected, option.Description);
        }
    }
}
