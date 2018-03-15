namespace Parser.Models
{
    abstract class OptionBase
    {
        string description;

        public abstract OptionType Type { get; }
        public string Full { get; protected set; }
        public string Abbr { get; protected set; }

        protected OptionBase()
        {
        }

        protected OptionBase(string full, string abbr, string description = null)
        {
            Full = full;
            Abbr = abbr;
            Description = description;
        }

        public string Description
        {
            get { return description?.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " "); }
            set { description = value; }
        }

        protected bool Equals(OptionBase other)
        {
            return Type == other.Type &&
                   Full != null && string.Equals(Full, other.Full) ||
                   Abbr != null && string.Equals(Abbr, other.Abbr);
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

        public override string ToString()
        {
            return (
                $"{(string.IsNullOrWhiteSpace(Full) ? "" : Full)}" +
                $"{(string.IsNullOrWhiteSpace(Abbr) ? "" : " " + Abbr)}" +
                $"{(string.IsNullOrWhiteSpace(Description) ? "" : " " + Description)}"
            ).Trim();
        }
    }
}
