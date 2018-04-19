using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParserBuilderTests
{
    public class when_build_a_parser
    {
        readonly ArgsParserBuilder builder;

        public when_build_a_parser()
        {
            builder = new ArgsParserBuilder();
        }

        [Fact]
        void should_build_a_parser_successfully_after_ending_command()
        {
            builder.BeginDefaultCommand().EndCommand();

            Assert.Null(Record.Exception(() => builder.Build()));
        }
    }
}
