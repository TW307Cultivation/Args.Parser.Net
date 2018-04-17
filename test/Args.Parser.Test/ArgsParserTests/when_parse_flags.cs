using System;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParserTests
{
    public class when_parse_flags
    {
        readonly ArgsParser parser;

        public when_parse_flags()
        {
            parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', null)
                .AddFlagOption("remove", 'r', null)
                .EndCommand()
                .Build();
        }

        [Fact]
        void should_throw_ArgumentNullException_when_args_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]
        void should_throw_ArgumentException_when_args_has_null_value()
        {
            Assert.Throws<ArgumentException>(() => parser.Parse(new string[] {null}));
        }

        [Theory]
        [InlineData("what_is_this")]
        [InlineData("--not-defined-flags")]
        [InlineData("-a")]
        void should_parse_failed_when_arg_is_invalid_or_undefined(string arg)
        {
            var result = parser.Parse(new[] {"--flag", arg});

            Assert.False(result.IsSuccess);
            Assert.Null(result.Command);
            Assert.NotNull(result.Error);
            Assert.Equal(ArgsParsingErrorCode.FreeValueNotSupported, result.Error.Code);
            Assert.Equal(arg, result.Error.Trigger);
        }

        [Fact]
        void should_parse_failed_when_specify_duplicate_flags()
        {
            var result = parser.Parse(new[] {"--flag", "-f"});

            Assert.False(result.IsSuccess);
            Assert.Null(result.Command);
            Assert.NotNull(result.Error);
            Assert.Equal(ArgsParsingErrorCode.DuplicateFlagsInArgs, result.Error.Code);
            Assert.Equal("-f", result.Error.Trigger);
        }

        [Fact]
        void should_throw_ArgumentNullException_when_flag_is_null()
        {
            var result = parser.Parse(new[] {"--flag"});

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Command);
            Assert.Null(result.Command.Symbol);
            Assert.Null(result.Error);
            Assert.Throws<ArgumentNullException>(() => result.GetFlagValue(null));
        }

        [Fact]
        void should_throw_InvalidOperationException_when_get_flag_value_on_parse_failed()
        {
            var result = parser.Parse(new[] {"-flag"});

            Assert.False(result.IsSuccess);
            Assert.Null(result.Command);
            Assert.Throws<InvalidOperationException>(() => result.GetFlagValue("--flag"));
        }

        [Theory]
        [InlineData("what_is_this")]
        [InlineData("-rf")]
        [InlineData("--not-defined-flags")]
        [InlineData("-a")]
        void should_throw_ArgumentException_when_flag_is_invalid_or_undefined(string flag)
        {
            var result = parser.Parse(new[] {"--flag"});

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Command);
            Assert.Null(result.Command.Symbol);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue(flag));
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
        void should_get_flag_value_after_parsing_and_ignore_case(string argument, string flag)
        {
            var result = parser.Parse(new[] {argument});

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Command);
            Assert.Null(result.Command.Symbol);
            Assert.Null(result.Error);
            Assert.True(result.GetFlagValue(flag));
        }

        [Fact]
        void should_can_parse_multiple_args()
        {
            var result = parser.Parse(new[] {"--flag", "-r"});

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Command);
            Assert.Null(result.Command.Symbol);
            Assert.Null(result.Error);
            Assert.True(result.GetFlagValue("-f"));
            Assert.True(result.GetFlagValue("--remove"));
        }
    }
}
