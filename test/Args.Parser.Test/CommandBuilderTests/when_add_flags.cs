﻿using System;
using Args.Parser.Commands;
using Args.Parser.Core;
using Xunit;

namespace Args.Parser.Test.CommandBuilderTests
{
    public class when_add_flags
    {
        readonly CommandBuilder builder;

        public when_add_flags()
        {
            builder = new ArgsParserBuilder().BeginDefaultCommand();
        }

        [Fact]
        void should_throw_ArgumentException_when_both_full_and_abbr_form_are_null()
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(null, null, null));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("abc*&")]
        [InlineData("-abc")]
        void should_throw_ArgumentException_when_full_form_is_invalid(string fullForm)
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullForm, null, null));
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('*')]
        [InlineData('0')]
        void should_throw_ArgumentException_when_abbr_form_is_invalid(char abbrForm)
        {
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(null, abbrForm, null));
        }

        [Theory]
        [InlineData("flag", 'f')]
        [InlineData("flag", null)]
        [InlineData(null, 'f')]
        void should_throw_ArgumentException_when_add_duplicate_option(string fullForm, char? abbrForm)
        {
            builder.AddFlagOption("flag", 'f', null);

            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullForm, abbrForm, null));
        }

        [Fact]
        void should_can_add_multiple_options()
        {
            builder.AddFlagOption("flag1", null, null);

            Assert.Null(Record.Exception(() => builder.AddFlagOption("flag2", null, null)));
        }

        [Theory]
        [InlineData("flag", null, null)]
        [InlineData(null, 'f', null)]
        [InlineData("flag", 'f', null)]
        [InlineData("flag", 'f', "description")]
        void should_end_command_successfully_when_arguments_are_valid(string fullForm, char? abbrForm, string description)
        {
            Assert.Null(Record.Exception(() => builder.AddFlagOption(fullForm, abbrForm, description).EndCommand()));
        }
    }
}
