﻿namespace Args.Parser
{
    /// <summary>
    /// Error types.
    /// </summary>
    public enum ArgsParsingErrorCode
    {
        /// <summary>
        /// The argument is invalid or undefined.
        /// </summary>
        FreeValueNotSupported,

        /// <summary>
        /// The argument is not defined in options.
        /// </summary>
        UndefinedOption,

        /// <summary>
        /// The argument has been specified before.
        /// </summary>
        DuplicateFlagsInArgs,

        /// <summary>
        /// The option's both full form and abbreviation form are empty.
        /// </summary>
        EmptyOption
    }
}
