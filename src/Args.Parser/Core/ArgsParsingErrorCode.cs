namespace Args.Parser.Core
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
        /// The argument has been specified before.
        /// </summary>
        DuplicateFlagsInArgs
    }
}
