namespace Parser.Models
{
    abstract class OptionBase
    {
        public abstract OptionType Type { get; }

        public string Full { get; set; }

        public char? Abbr { get; set; }

        public string Description { get; set; }

        protected bool Equals(OptionBase other)
        {
            return Full != null && string.Equals(Full, other.Full) ||
                   Abbr.HasValue && other.Abbr.HasValue && Abbr.Value == other.Abbr.Value;
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
