namespace Parser
{
    /// <summary>
    /// Error types.
    /// </summary>
    public enum ParsingErrorCode
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
        DuplicatedOption
    }
}
