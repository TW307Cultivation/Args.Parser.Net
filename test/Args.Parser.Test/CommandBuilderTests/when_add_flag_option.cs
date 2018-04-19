using System;
using Args.Parser.Commands;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.CommandBuilderTests
{
    public class when_add_flag_option
    {
        readonly CommandBuilder builder;

        public when_add_flag_option()
        {
            builder = new ArgsParserBuilder().BeginDefaultCommand();
        }

        [Fact]
        void should_throw_ArgumentException_when_both_full_and_abbr_form_are_null()
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(null, null));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("abc*&")]
        [InlineData("-abc")]
        void should_throw_ArgumentException_when_full_form_is_invalid(string fullForm)
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullForm, null));
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('*')]
        [InlineData('0')]
        void should_throw_ArgumentException_when_abbr_form_is_invalid(char abbrForm)
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(null, abbrForm));
        }

        [Theory]
        [InlineData("recurse", 'r')]
        [InlineData("recurse", null)]
        [InlineData(null, 'r')]
        void should_throw_ArgumentException_when_add_duplicate_option(string fullForm, char? abbrForm)
        {
            builder.AddFlagOption("recurse", 'r');

            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullForm, abbrForm));
        }

        [Theory]
        [InlineData("recurse", null, null)]
        [InlineData(null, 'r', null)]
        [InlineData("recurse", 'r', null)]
        [InlineData("recurse", 'r', "description")]
        void should_add_option_successfully_when_arguments_are_valid(string fullForm, char? abbrForm, string description)
        {
            Assert.Null(Record.Exception(() => builder.AddFlagOption(fullForm, abbrForm, description)));
        }

        [Fact]
        void should_support_adding_multiple_options()
        {
            builder.AddFlagOption("recurse", 'r');

            Assert.Null(Record.Exception(() => builder.AddFlagOption("force", null)));
            Assert.Null(Record.Exception(() => builder.AddFlagOption("r", null)));
        }
    }
}
