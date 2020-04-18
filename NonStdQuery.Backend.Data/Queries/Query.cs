using System.Collections.Generic;

namespace NonStdQuery.Backend.Data.Queries
{
    public class Query
    {
        public List<string> SelectAttributes { get; set; } = new List<string>();
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public List<SortAttribute> SortAttributes { get; set; } = new List<SortAttribute>();
        
        public override bool Equals(object obj)
        {
            if (obj is Query q)
            {
                return Equals(q);
            }
            return base.Equals(obj);
        }

        public bool Equals(Query other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, null))
            {
                return true;
            }

            return SelectAttributes == other.SelectAttributes
                   && Conditions == other.Conditions
                   && SortAttributes == other.SortAttributes;
        }

        public override int GetHashCode()
        {
            var hash = 13;
            
            unchecked
            {
                foreach (var selectAttribute in SelectAttributes)
                {
                    hash *= 7;
                    hash += selectAttribute.GetHashCode();
                }

                foreach (var condition in Conditions)
                {
                    hash *= 7;
                    hash += condition.GetHashCode();
                }

                foreach (var sortAttribute in SortAttributes)
                {
                    hash *= 7;
                    hash += sortAttribute.GetHashCode();
                }
            }

            return hash;
        }
    }
}
