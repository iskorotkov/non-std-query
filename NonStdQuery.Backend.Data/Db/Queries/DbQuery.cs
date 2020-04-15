using System.Collections.Generic;

namespace NonStdQuery.Backend.Data.Db.Queries
{
    public class DbQuery
    {
        public string Sql { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }

        public DbQuery(string sql, Dictionary<string, object> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }
}
