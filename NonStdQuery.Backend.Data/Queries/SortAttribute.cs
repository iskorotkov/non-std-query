namespace NonStdQuery.Backend.Data.Queries
{
    public class SortAttribute
    {
        public string AttributeName { get; set; }
        public SortDirection Direction { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SortAttribute x)
            {
                return Equals(x);
            }
            return base.Equals(obj);
        }

        public bool Equals(SortAttribute other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, null))
            {
                return true;
            }

            return AttributeName == other.AttributeName && Direction == other.Direction;
        }

        public override int GetHashCode()
        {
            var hash = 13;
            
            hash *= 7;
            hash += AttributeName.GetHashCode();

            hash *= 7;
            hash += Direction.GetHashCode();

            return hash;
        }
    }
}
