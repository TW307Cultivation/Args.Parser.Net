using System;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParsingResultTests
{
    public class when_get_flag_value
    {
        readonly ArgsParser parser;

        public when_get_flag_value()
        {
            parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", 'r')
                .AddFlagOption("force", 'f')
                .AddFlagOption("confirm", 'c')
                .EndCommand()
                .Build();
        }

        [Fact]
        void should_throw_ArgumentNullException_when_arg_is_null()
        {
            var result = parser.Parse(new[] {"--recurse"});

            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentNullException>(() => result.GetFlagValue(null));
        }

        [Fact]
        void should_throw_InvalidOperationException_when_parse_failed()
        {
            var result = parser.Parse(new[] {"-flag"});

            Assert.False(result.IsSuccess);
            Assert.Throws<InvalidOperationException>(() => result.GetFlagValue("--flag"));
        }

        [Theory]
        [InlineData("what_is_this")]
        [InlineData("--not-defined-flags")]
        [InlineData("-rf")]
        [InlineData("-a")]
        void should_throw_ArgumentException_when_arg_is_invalid_or_undefined(string arg)
        {
            var result = parser.Parse(new[] {"--recurse"});

            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue(arg));
        }

        [Fact]
        void should_get_flag_value_ignore_case()
        {
            var result = parser.Parse(new[] {"-rf"});

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--recurse"));
            Assert.True(result.GetFlagValue("--RECURSE"));
            Assert.True(result.GetFlagValue("-r"));
            Assert.True(result.GetFlagValue("-R"));
            Assert.True(result.GetFlagValue("-f"));
            Assert.False(result.GetFlagValue("-c"));
        }
    }
}
