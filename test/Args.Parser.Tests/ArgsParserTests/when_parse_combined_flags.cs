using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Tests.ArgsParserTests
{
    public class when_parse_combined_flags
    {
        readonly ArgsParser parser;

        public when_parse_combined_flags()
        {
            parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', null)
                .AddFlagOption("remove", 'r', null)
                .AddFlagOption("message", 'm', null)
                .EndCommand()
                .Build();
        }

        [Fact]
        void should_parse_failed_when_arg_is_undefined()
        {
            var result = parser.Parse(new[] {"-rfa"});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.FreeValueNotSupported, result.Error.Code);
            Assert.Equal("-rfa", result.Error.Trigger);
        }

        [Fact]
        void should_parse_failed_when_specify_duplicate_flags()
        {
            var result = parser.Parse(new[] {"-rf", "--flag"});

            Assert.False(result.IsSuccess);
            Assert.Equal(ArgsParsingErrorCode.DuplicateFlagsInArgs, result.Error.Code);
            Assert.Equal("--flag", result.Error.Trigger);
        }

        [Fact]
        void should_can_parse_combined_args()
        {
            var result = parser.Parse(new[] {"-rf"});

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
            Assert.True(result.GetFlagValue("--remove"));
            Assert.False(result.GetFlagValue("--message"));
            Assert.False(result.GetFlagValue("-m"));
            Assert.Null(result.Error);
        }
    }
}
