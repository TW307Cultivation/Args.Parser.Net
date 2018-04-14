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
        void should_throw_InvalidOperationException_when_add_multiple_default_command()
        {
            builder.BeginDefaultCommand();
            Assert.Throws<InvalidOperationException>(() => builder.BeginDefaultCommand());
        }
    }
}
