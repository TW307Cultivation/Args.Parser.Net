using Args.Parser.Commands;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.CommandBuilderTests
{
    public class when_end_command
    {
        readonly CommandBuilder builder;

        public when_end_command()
        {
            builder = new ArgsParserBuilder().BeginDefaultCommand();
        }

        [Fact]
        void should_end_command_successfully_after_adding_options()
        {
            builder.AddFlagOption("recurse", 'r');

            Assert.Null(Record.Exception(() => builder.EndCommand()));
        }
    }
}
