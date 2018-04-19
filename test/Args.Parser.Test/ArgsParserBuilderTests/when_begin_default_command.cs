using System;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParserBuilderTests
{
    public class when_begin_default_command
    {
        readonly ArgsParserBuilder builder;

        public when_begin_default_command()
        {
            builder = new ArgsParserBuilder();
        }

        [Fact]
        void should_begin_default_command_successfully()
        {
            Assert.Null(Record.Exception(() => builder.BeginDefaultCommand()));
        }

        [Fact]
        void should_throw_InvalidOperationException_when_begin_default_command_times()
        {
            builder.BeginDefaultCommand().EndCommand();

            Assert.Throws<InvalidOperationException>(() => builder.BeginDefaultCommand().EndCommand());
        }
    }
}
