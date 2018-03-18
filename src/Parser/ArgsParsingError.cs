namespace Parser
{
    /// <summary>
    /// The details of parsing error.
    /// </summary>
    public class ArgsParsingError
    {
        /// <summary>
        /// The error type.
        /// </summary>
        public ArgsErrorCode Code { get; set; }

        /// <summary>
        /// The argument that cause error when parse.
        /// </summary>
        public string Trigger { get; set; }
    }
}
