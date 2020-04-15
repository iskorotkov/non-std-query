using System.Collections.Generic;

namespace NonStdQuery.Backend.Data.Queries
{
    public class Query
    {
        public List<string> SelectAttributes { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<string> SortAttributes { get; set; }
    }
}
