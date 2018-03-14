using Xunit;

namespace Parser.Tests
{
    public class ArgsParserBuilderTests
    {
        [Fact]
        void should_add_flag_option_only_with_full_form()
        {
            var parser = new ArgsParserBuilder()
                .AddFlagOption("flag")
                .Build();

            Assert.NotNull(parser);
        }

        [Fact]
        void should_add_flag_option_only_with_abbreviation_form()
        {
            var parser = new ArgsParserBuilder()
                .AddFlagOption('f')
                .Build();

            Assert.NotNull(parser);
        }

        [Fact]
        void should_add_flag_option_with_full_and_abbreviation_form()
        {
            var parser = new ArgsParserBuilder()
                .AddFlagOption("flag", 'f')
                .Build();

            Assert.NotNull(parser);
        }

        [Fact]
        void should_add_flag_option_with_full_and_abbreviation_form_and_description()
        {
            var parser = new ArgsParserBuilder()
                .AddFlagOption("flag", 'f', "this is description")
                .Build();

            Assert.NotNull(parser);
        }

        [Fact]
        void should_can_not_add_flag_option_more_than_once()
        {
            var builder = new ArgsParserBuilder()
                .AddFlagOption("flag");

            var exception = Assert.Throws<ParserException>(() => builder.AddFlagOption(""));
            Assert.NotNull(exception);
            Assert.Equal("Can only specify flag once.", exception.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("", ' ')]
        [InlineData(" ", ' ')]
        [InlineData(null, ' ')]
        void should_throw_exception_when_both_full_form_and_abbreviation_form_are_empty(string fullForm, char? abbreviationForm)
        {
            var builder = new ArgsParserBuilder();

            var exception = Assert.Throws<ParserException>(() => builder.AddFlagOption(fullForm, abbreviationForm));
            Assert.NotNull(exception);
            Assert.Equal("Must specify flag with full form or abbreviation form.", exception.Message);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("abc*&")]
        [InlineData("-abc")]
        void should_throw_exception_when_full_form_is_invalid(string fullForm)
        {
            var builder = new ArgsParserBuilder();

            var exception = Assert.Throws<ParserException>(() => builder.AddFlagOption(fullForm, 'f'));
            Assert.NotNull(exception);
            Assert.Equal("Invalid full form.", exception.Message);
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('*')]
        [InlineData('0')]
        void should_throw_exception_when_abbreviation_form_is_invalid(char abbreviation)
        {
            var builder = new ArgsParserBuilder();

            var exception = Assert.Throws<ParserException>(() => builder.AddFlagOption("flag", abbreviation));
            Assert.NotNull(exception);
            Assert.Equal("Invalid abbreviation form.", exception.Message);
        }
    }
}
