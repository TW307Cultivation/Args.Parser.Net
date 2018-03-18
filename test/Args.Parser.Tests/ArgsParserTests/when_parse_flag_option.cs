using System;
using Xunit;

namespace Args.Parser.Tests.ArgsParserTests
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
        void should_get_flag_false_when_arguments_is_empty(string argument)
        {
            var result = parser.Parse(new string[] { });

            Assert.True(result.IsSuccess);
            Assert.False(result.GetFlagValue(argument));
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
            Assert.Null(result.Error);
        }

        [Fact]
        void should_throw_argument_null_exception_when_flag_is_null()
        {
            var result = parser.Parse(new[] {"--flag"});

            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentNullException>(() => result.GetFlagValue(null));
        }

        [Theory]
        [InlineData("-flag")]
        [InlineData("---flag")]
        [InlineData("f")]
        void should_parse_failed_when_argument_invalid(string argument)
        {
            var result = parser.Parse(new[] {argument});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.InvalidArgument, result.Error.Code);
            Assert.Equal(argument, result.Error.Trigger);
        }

        [Fact]
        void should_parse_failed_when_argument_undefined()
        {
            var result = parser.Parse(new[] {"--new"});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.UndefinedOption, result.Error.Code);
            Assert.Equal("--new", result.Error.Trigger);
        }

        [Fact]
        void should_parse_failed_when_specify_one_flag_more_than_once()
        {
            var result = parser.Parse(new[] {"--flag", "-f"});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.DuplicateOption, result.Error.Code);
            Assert.Equal("-f", result.Error.Trigger);
        }
    }
}
