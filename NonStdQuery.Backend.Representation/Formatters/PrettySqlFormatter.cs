using System;
using System.Linq;
using NonStdQuery.Backend.Data.Db.Queries;

namespace NonStdQuery.Backend.Representation.Formatters
{
    public class PrettySqlFormatter
    {
        public string ToPrettySql(DbQuery query)
        {
            var parameters = query.Parameters
                .OrderByDescending(kv => kv.Key);
            var prettySql = query.Sql;
            
            foreach (var parameter in parameters)
            {
                if (parameter.Value is string s)
                {
                    prettySql = prettySql.Replace(parameter.Key, $"'{s}'");
                }
                else if (parameter.Value is DateTime dt)
                {
                    prettySql = prettySql.Replace(parameter.Key, "{ " + dt + " }");
                }
                else
                {
                    prettySql = prettySql.Replace(parameter.Key, parameter.Value.ToString());
                }
            }

            return prettySql;
        }
    }
}
