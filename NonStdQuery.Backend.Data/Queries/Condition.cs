namespace NonStdQuery.Backend.Data.Queries
{
    public class Condition
    {
        public string AttributeName { get; set; }
        public object Value { get; set; }
        public Operation Operation { get; set; }
        public LinkMethod Link { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Condition x)
            {
                return Equals(x);
            }
            return base.Equals(obj);
        }

        public bool Equals(Condition other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, null))
            {
                return true;
            }

            return AttributeName == other.AttributeName
                   && Value == other.Value
                   && Operation == other.Operation
                   && Link == other.Link;
        }

        public override int GetHashCode()
        {
            var hash = 13;

            hash *= 7;
            hash += AttributeName.GetHashCode();

            hash *= 7;
            hash += Value.GetHashCode();

            hash *= 7;
            hash += Operation.GetHashCode();

            hash *= 7;
            hash += Link.GetHashCode();
            
            return hash;
        }
    }
}
