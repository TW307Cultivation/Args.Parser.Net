namespace Args.Parser.Core
{
    /// <summary>
    /// The details of parsing error.
    /// </summary>
    public class ArgsParsingError
    {
        /// <summary>
        /// The error type.
        /// </summary>
        public ArgsParsingErrorCode Code { get; }

        /// <summary>
        /// The argument that cause error when parse.
        /// </summary>
        public string Trigger { get; }

        internal ArgsParsingError(ArgsParsingErrorCode code, string trigger)
        {
            Code = code;
            Trigger = trigger;
        }
    }
}
