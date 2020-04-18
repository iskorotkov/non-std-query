namespace NonStdQuery.Backend.Data.Db.Queries
{
    public class DbAttribute
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public Db.Queries.DbType Type { get; set; }

        public bool Equals(DbAttribute obj)
        {
            return TableName == obj.TableName
                   && ColumnName == obj.ColumnName
                   && Type == obj.Type;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbAttribute x)
            {
                return Equals(x);
            }
            return base.Equals(obj);
        }
    }
}
