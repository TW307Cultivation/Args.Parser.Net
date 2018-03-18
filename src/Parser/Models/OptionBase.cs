using System;

namespace Parser.Models
{
    abstract class OptionBase
    {
        public abstract OptionType Type { get; }
        public string Full { get; protected set; }
        public string Abbr { get; protected set; }
        public string Description { get; set; }

        protected OptionBase()
        {
        }

        protected OptionBase(string full, string abbr, string description = null)
        {
            Full = full;
            Abbr = abbr;
            Description = description;
        }

        protected bool Equals(OptionBase other)
        {
            return Type == other.Type &&
                   Full != null && string.Equals(Full, other.Full, StringComparison.OrdinalIgnoreCase) ||
                   Abbr != null && string.Equals(Abbr, other.Abbr, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OptionBase) obj);
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