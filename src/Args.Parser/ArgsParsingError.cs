namespace Args.Parser
{
    /// <summary>
    /// The details of parsing error.
    /// </summary>
    public class ArgsParsingError
    {
        /// <summary>
        /// The error type.
        /// </summary>
        public ArgsParsingErrorCode Code { get; set; }

        /// <summary>
        /// The argument that cause error when parse.
        /// </summary>
        public string Trigger { get; set; }
    }
}
