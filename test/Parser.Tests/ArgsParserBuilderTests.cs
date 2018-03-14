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

            var flagException = Assert.Throws<FlagException>(() => builder.AddFlagOption(""));
            Assert.NotNull(flagException);
            Assert.Equal("Can not add flag option more than once", flagException.Message);
        }
    }
}
