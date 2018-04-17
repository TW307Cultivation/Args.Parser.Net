using System;
using System.Linq;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.ArgsParserTests
{
    public class when_get_option_metadatas_from_command
    {
        [Theory]
        [InlineData("description")]
        [InlineData(null)]
        void should_get_option_metadata(string description)
        {
            var parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddFlagOption("flag", 'f', description)
                .EndCommand()
                .Build();

            var result = parser.Parse(new[] {"--flag"});
            Assert.True(result.IsSuccess);
            Assert.Null(result.Command.Symbol);
            Assert.Empty(result.Command.Description);

            var options = result.Command.GetRegisteredOptionsMetadata().ToArray();
            var option = options.Single(d => d.SymbolMetadata.FullForm.Equals("flag", StringComparison.OrdinalIgnoreCase));
            Assert.Equal("flag", option.SymbolMetadata.FullForm);
            Assert.Equal('f', option.SymbolMetadata.Abbreviation);
            Assert.Equal(description ?? string.Empty, option.Description);
        }
    }
}
