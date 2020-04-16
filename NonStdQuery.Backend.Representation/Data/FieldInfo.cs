using NonStdQuery.Backend.Data.Db.Queries;

namespace NonStdQuery.Backend.Representation.Data
{
    public class FieldInfo
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
    }
}
