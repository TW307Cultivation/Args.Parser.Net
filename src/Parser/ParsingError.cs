namespace Parser
{
    /// <summary>
    /// The details of parsing error.
    /// </summary>
    public class ParsingError
    {
        /// <summary>
        /// The error type.
        /// </summary>
        public ParsingErrorCode Code { get; set; }

        /// <summary>
        /// The argument that cause error when parse.
        /// </summary>
        public string Trigger { get; set; }
    }
}
