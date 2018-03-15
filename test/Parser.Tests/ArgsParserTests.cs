using Parser.Exceptions;
using Xunit;

namespace Parser.Tests
{
    public class ArgsParserTests
    {
        readonly ArgsParser parser;

        public ArgsParserTests()
        {
            parser = new ArgsParserBuilder()
                .AddFlagOption("flag", 'f', "this is description")
                .Build();
        }

        [Theory]
        [InlineData("-flag")]
        [InlineData("---flag")]
        [InlineData("f")]
        void should_throw_exception_when_argument_invalid(string argument)
        {
            var exception = Assert.Throws<ParserException>(() => parser.Parse(new[] {argument}));

            Assert.NotNull(exception);
            Assert.Equal($"Invalid argument format: {argument}", exception.Message);
        }

        [Fact]
        void should_throw_exception_when_argument_undefined()
        {
            var exception = Assert.Throws<ParserException>(() => parser.Parse(new[] {"--new"}));

            Assert.NotNull(exception);
            Assert.Equal("Invalid argument format: --new", exception.Message);
        }

        [Fact]
        void should_throw_exception_when_contains_mutiple_arguments()
        {
            var exception = Assert.Throws<ParserException>(() => parser.Parse(new[] {"--flag", "-f"}));

            Assert.NotNull(exception);
            Assert.Equal("Duplicated arguments: --flag -f", exception.Message);
        }

        [Theory]
        [InlineData("--flag", "-f")]
        [InlineData("-f", "--flag")]
        [InlineData("--FLAG", "-f")]
        [InlineData("-F", "--flag")]
        [InlineData("--flag", "-F")]
        [InlineData("-f", "--FLAG")]
        void should_get_flag_true_when_parse_success(string argument, string flagArgument)
        {
            var result = parser.Parse(new[] {argument});

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue(flagArgument));
        }

        [Theory]
        [InlineData("--flag")]
        [InlineData("-f")]
        void should_get_flag_false_when_arguments_is_empty(string argument)
        {
            var result = parser.Parse(new string[] { });

            Assert.True(result.IsSuccess);
            Assert.False(result.GetFlagValue(argument));
        }

        [Theory]
        [InlineData("--flag")]
        [InlineData("-f")]
        void should_get_flag_false_when_arguments_is_null(string argument)
        {
            var result = parser.Parse(null);

            Assert.True(result.IsSuccess);
            Assert.False(result.GetFlagValue(argument));
        }
    }
}
