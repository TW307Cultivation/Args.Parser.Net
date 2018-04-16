using System;
using System.Text.RegularExpressions;
using Args.Parser.Core;
using Args.Parser.Exceptions;

namespace Args.Parser.Options
{
    class OptionSymbol
    {
        public string Full { get;}

        public char? Abbr { get; }

        static readonly Regex FullOptionRegex = new Regex(@"^[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        static readonly Regex AbbrOptionRegex = new Regex(@"^[a-zA-Z]{1}$", RegexOptions.Compiled);

        const string FullArgPrefix = "--";

        const string AbbrArgPrefix = "-";

        public OptionSymbol(string full, char? abbr)
        {
            if (full == null && abbr == null)
            {
                throw new ArgumentException("The option need a full form or abbreviation form.");
            }
            if (full != null && !FullOptionRegex.Match(full).Success)
            {
                throw new ArgumentException("This full form argument is invalid.");
            }
            if (abbr != null && !AbbrOptionRegex.Match(abbr.ToString()).Success)
            {
                throw new ArgumentException("This abbreviation form argument is invalid.");
            }

            Full = full;
            Abbr = abbr;
        }

        public OptionSymbol(string arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            if (arg.StartsWith(FullArgPrefix) && FullOptionRegex.Match(arg.Substring(2)).Success)
            {
                Full = arg.Substring(2);
                return;
            }
            if (arg.StartsWith(AbbrArgPrefix) && AbbrOptionRegex.Match(arg.Substring(1)).Success)
            {
                Abbr = arg[1];
                return;
            }
            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
        }

        bool Equals(OptionSymbol other)
        {
            return Full != null && string.Equals(Full, other.Full, StringComparison.OrdinalIgnoreCase) ||
                   Abbr != null && string.Equals(Abbr.ToString(), other.Abbr.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((OptionSymbol) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Full != null ? Full.GetHashCode() : 0) * 397) ^ Abbr.GetHashCode();
            }
        }
    }
}
