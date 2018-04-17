using System;
using System.Text.RegularExpressions;
using Args.Parser.Commands;
using Args.Parser.Core;
using Args.Parser.Exceptions;

namespace Args.Parser.Options
{
    class OptionSymbol : IOptionSymbolMetadata
    {
        public string FullForm { get;}

        public char? Abbreviation { get; }

        static readonly Regex FullOptionRegex = new Regex(@"^[a-zA-Z\d_]+[a-zA-Z\d_-]*$", RegexOptions.Compiled);

        static readonly Regex AbbrOptionRegex = new Regex(@"^[a-zA-Z]{1}$", RegexOptions.Compiled);

        const string FullArgPrefix = "--";

        const string AbbrArgPrefix = "-";

        public OptionSymbol(string fullForm, char? abbreviation)
        {
            if (fullForm == null && abbreviation == null)
            {
                throw new ArgumentException("The option need a full form or abbreviation form.");
            }
            if (fullForm != null && !FullOptionRegex.Match(fullForm).Success)
            {
                throw new ArgumentException("This full form argument is invalid.");
            }
            if (abbreviation != null && !AbbrOptionRegex.Match(abbreviation.ToString()).Success)
            {
                throw new ArgumentException("This abbreviation form argument is invalid.");
            }

            FullForm = fullForm;
            Abbreviation = abbreviation;
        }

        public OptionSymbol(string arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            if (arg.StartsWith(FullArgPrefix) && FullOptionRegex.Match(arg.Substring(2)).Success)
            {
                FullForm = arg.Substring(2);
                return;
            }
            if (arg.StartsWith(AbbrArgPrefix) && AbbrOptionRegex.Match(arg.Substring(1)).Success)
            {
                Abbreviation = arg[1];
                return;
            }
            throw new ArgsParsingException(ArgsParsingErrorCode.FreeValueNotSupported, arg);
        }

        bool Equals(OptionSymbol other)
        {
            return FullForm != null && string.Equals(FullForm, other.FullForm, StringComparison.OrdinalIgnoreCase) ||
                   Abbreviation != null && string.Equals(Abbreviation.ToString(), other.Abbreviation.ToString(), StringComparison.OrdinalIgnoreCase);
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
                return ((FullForm != null ? FullForm.GetHashCode() : 0) * 397) ^ Abbreviation.GetHashCode();
            }
        }

    }
}
