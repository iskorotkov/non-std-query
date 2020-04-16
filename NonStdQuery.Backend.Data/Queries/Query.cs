using System.Collections.Generic;

namespace NonStdQuery.Backend.Data.Queries
{
    public class Query
    {
        public List<string> SelectAttributes { get; set; } = new List<string>();
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public List<SortAttribute> SortAttributes { get; set; } = new List<SortAttribute>();
    }
}
