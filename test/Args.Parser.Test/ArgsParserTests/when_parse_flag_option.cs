using System;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParserTests
{
    public class when_parse_flag_option
    {
        readonly ArgsParser parser;

        public when_parse_flag_option()
        {
            parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("recurse", 'r')
                .AddFlagOption("force", 'f')
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
        [InlineData("-rfa")]
        void should_parse_failed_when_arg_is_invalid_or_undefined(string arg)
        {
            var result = parser.Parse(new[] {arg});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.FreeValueNotSupported, result.Error.Code);
            Assert.Equal(arg, result.Error.Trigger);
        }

        [Fact]
        void should_parse_failed_when_specify_duplicate_flags()
        {
            var result = parser.Parse(new[] {"-rf", "-f"});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.DuplicateFlagsInArgs, result.Error.Code);
            Assert.Equal("-f", result.Error.Trigger);
        }

        [Theory]
        [InlineData("--recurse", "--force")]
        [InlineData("--RECURSE", "--FORCE")]
        [InlineData("-r", "-f")]
        [InlineData("-R", "-F")]
        [InlineData("-rf")]
        [InlineData("-RF")]
        void should_support_parsing_flag_option_ignore_case(params string[] args)
        {
            var result = parser.Parse(args);

            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
        }
    }
}
