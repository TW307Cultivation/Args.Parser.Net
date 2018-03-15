using System.Text.RegularExpressions;

namespace Parser.Models
{
    static class Config
    {
        internal static readonly Regex FullOptionRegex =
            new Regex(@"^[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        internal static readonly Regex AbbrOptionRegex = new Regex(@"^[a-zA-Z]{1}$", RegexOptions.Compiled);

        internal static readonly Regex
            FullArgRegex = new Regex(@"^--[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        internal static readonly Regex AbbrArgRegex = new Regex(@"^-[a-zA-Z]{1}$", RegexOptions.Compiled);

        internal static readonly string FullArgPrefix = "--";

        internal static readonly string AbbrArgPrefix = "-";
    }
}
