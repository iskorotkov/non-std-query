namespace NonStdQuery.Backend.Data.Db.Queries
{
    public class DbQuery
    {
        public string Sql { get; private set; }

        public DbQuery(string sql)
        {
            Sql = sql;
        }
    }
}
