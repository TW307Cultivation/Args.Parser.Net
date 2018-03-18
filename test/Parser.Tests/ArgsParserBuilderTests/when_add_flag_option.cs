using Parser.Exceptions;
using Xunit;

namespace Parser.Tests.ArgsParserBuilderTests
{
    public class when_parser_builder_add_flag_option
    {
        readonly ArgsParserBuilder builder = new ArgsParserBuilder();

        [Theory]
        [InlineData("flag", null, null)]
        [InlineData(null, 'f', null)]
        [InlineData("flag", 'f', null)]
        [InlineData("flag", 'f', "description")]
        void should_add_flag_option(string fullForm, char? abbrForm, string description)
        {
            var parser = builder
                .AddFlagOption(fullForm, abbrForm, description)
                .Build();

            Assert.NotNull(parser);
        }

        [Fact]
        void should_can_not_add_flag_option_more_than_once()
        {
            builder.AddFlagOption("flag", null, null);

            var ex = Assert.Throws<ParserException>(() => builder.AddFlagOption("flag", null, null));

            Assert.NotNull(ex);
            Assert.Equal("Can only specify flag option once.", ex.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        void should_throw_error_when_both_full_form_and_abbr_form_are_empty(string fullForm, char? abbrForm)
        {
            var ex = Assert.Throws<ParserException>(() => builder.AddFlagOption(fullForm, abbrForm, null));

            Assert.NotNull(ex);
            Assert.Equal("Must specify flag option with full form or abbreviation form", ex.Message);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("abc*&")]
        [InlineData("-abc")]
        void should_throw_error_when_full_form_is_invalid(string fullForm)
        {
            var ex = Assert.Throws<ParserException>(() => builder.AddFlagOption(fullForm, null, null));

            Assert.NotNull(ex);
            Assert.Equal($"Invalid full form: {fullForm}", ex.Message);
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('*')]
        [InlineData('0')]
        [InlineData('\r')]
        [InlineData('\n')]
        void should_throw_error_when_abbreviation_form_is_invalid(char abbrForm)
        {
            var ex = Assert.Throws<ParserException>(() => builder.AddFlagOption(null, abbrForm, null));

            Assert.NotNull(ex);
            Assert.Equal($"Invalid abbreviation form: {abbrForm}", ex.Message);
        }
    }
}
