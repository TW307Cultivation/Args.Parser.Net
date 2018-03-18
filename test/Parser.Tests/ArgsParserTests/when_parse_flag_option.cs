using Parser.Exceptions;
using Xunit;

namespace Parser.Tests.ArgsParserTests
{
    public class ArgsParserTests
    {
        readonly ArgsParser parser;

        public ArgsParserTests()
        {
            parser = new ArgsParserBuilder()
                .AddFlagOption("flag", 'f', "description")
                .Build();
        }

        [Theory]
        [InlineData("--flag", "--flag")]
        [InlineData("--FLAG", "--flag")]
        [InlineData("--flag", "-f")]
        [InlineData("--flag", "-F")]
        [InlineData("-f", "-f")]
        [InlineData("-F", "-f")]
        [InlineData("-f", "--flag")]
        [InlineData("-f", "--FLAG")]
        void should_parse_successful_and_get_flag_value(string argument, string flag)
        {
            var result = parser.Parse(new[] {argument});

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue(flag));
            Assert.Null(result.Error);
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

        [Theory]
        [InlineData("-flag")]
        [InlineData("---flag")]
        [InlineData("f")]
        void should_parse_failed_when_argument_invalid(string argument)
        {
            var result = parser.Parse(new[] {argument});

//            Assert.NotNull(exception);
//            Assert.Equal($"Invalid argument format: {argument}", exception.Message);
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
        [InlineData("--flag")]
        [InlineData("-f")]
        void should_get_flag_false_when_arguments_is_empty(string argument)
        {
            var result = parser.Parse(new string[] { });

            Assert.True(result.IsSuccess);
            Assert.False(result.GetFlagValue(argument));
        }
    }
}
