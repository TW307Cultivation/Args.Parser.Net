using System.Text.RegularExpressions;

namespace Args.Parser.Models
{
    static class Config
    {
        public static readonly Regex FullOptionRegex = new Regex(@"^[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        public static readonly Regex AbbrOptionRegex = new Regex(@"^[a-zA-Z]{1}$", RegexOptions.Compiled);

        public static readonly Regex FullArgRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        public static readonly Regex AbbrArgRegex = new Regex(@"^-[a-zA-Z]+$", RegexOptions.Compiled);

        public const string FullArgPrefix = "--";

        public const string AbbrArgPrefix = "-";
    }
}
