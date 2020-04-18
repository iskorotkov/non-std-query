using NonStdQuery.Backend.Data.Db.Queries;
using System.Collections.Generic;

namespace NonStdQuery.Backend.Representation.Data
{
    public class Tooltip
    {
        public string FieldName { get; set; }
        public DbType Type { get; set; }
        public List<object> Items { get; set; } = new List<object>();
    }
}
