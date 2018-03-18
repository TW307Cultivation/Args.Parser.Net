namespace Args.Parser
{
    /// <summary>
    /// Error types.
    /// </summary>
    public enum ArgsParsingErrorCode
    {
        /// <summary>
        /// The argument form is invalid.
        /// The full form argument need a prefix "--",
        /// and the abbreviation form need "-".
        /// </summary>
        InvalidArgument,

        /// <summary>
        /// The argument is not defined in options.
        /// </summary>
        UndefinedOption,

        /// <summary>
        /// The argument has been specified before.
        /// </summary>
        DuplicateOption,

        /// <summary>
        /// The option's both full form and abbreviation form are empty.
        /// </summary>
        EmptyOption
    }
}
