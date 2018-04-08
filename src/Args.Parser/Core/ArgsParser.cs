﻿using System;
using System.Collections.Generic;
using System.Linq;
using Args.Parser.Exceptions;
using Args.Parser.Models;

namespace Args.Parser.Core
{
    /// <summary>
    /// Argumnets Parser.
    /// </summary>
    public class ArgsParser
    {
        readonly HashSet<OptionBase> arguments = new HashSet<OptionBase>();
        readonly HashSet<OptionBase> options;

        internal ArgsParser(HashSet<OptionBase> options)
        {
            this.options = options ?? new HashSet<OptionBase>();
        }

        /// <summary>
        /// Parse arguments accordinating to options.
        /// </summary>
        /// <param name="args">
        /// Input arguments, full form or abbreviation form.
        /// </param>
        /// <returns>
        /// A parsing result <see cref="ArgsParsingResult"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The args is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The args has null value.
        /// </exception>
        public ArgsParsingResult Parse(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if (args.Any(e => e == null))
            {
                throw new ArgumentException(nameof(args));
            }

            try
            {
                foreach (var arg in args)
                {
                    BuildArguments(arg).ForEach(e =>
                    {
                        if (arguments.Any(a => a.Equals(e)))
                        {
                            throw new ArgsParsingException(ArgsParsingErrorCode.DuplicateFlagsInArgs, arg);
                        }
                        arguments.Add(e);
                    });
                }

                return new ArgsParsingResult(arguments, options);
            }
            catch (ArgsParsingException e)
            {
                return new ArgsParsingResult(new ArgsParsingError(e.Code, e.Trigger));
            }
        }

        List<FlagArgument> BuildArguments(string arg)
        {
            if (Config.FullArgRegex.Match(arg).Success)
            {
                return new List<FlagArgument>() {new FlagArgument(arg, options)};
            }
            if (Config.AbbrArgRegex.Match(arg).Success)
            {
                try
                {
                    return arg.Substring(1)
                        .Select(e => new FlagArgument($"{Config.AbbrArgPrefix}{e.ToString()}", options))
                        .ToList();
                }
                catch (ArgsParsingException e)
                {
                    throw new ArgsParsingException(e.Code, arg);
                }
            }
            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
        }
    }
}
