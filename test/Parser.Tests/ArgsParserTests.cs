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
    }
}
