using System;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Tests.ArgsParserBuilderTests
{
    public class when_add_commands
    {
        readonly ArgsParserBuilder builder;

        public when_add_commands()
        {
            builder = new ArgsParserBuilder();
        }

        [Fact]
        void should_throw_InvalidOperationException_when_define_multiple_default_command()
        {
            builder.BeginDefaultCommand().EndCommand();

            Assert.Throws<InvalidOperationException>(() => builder.BeginDefaultCommand().EndCommand());
        }

        [Fact]
        void should_build_a_parser_successfully_after_ending_command()
        {
            builder.BeginDefaultCommand().EndCommand();

            Assert.Null(Record.Exception(() => builder.Build()));
        }
    }
}
