using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using NonStdQuery.Backend.Data.Db;

namespace NonStdQuery.Backend.Data.JoinResolving
{
    public class JoinResolver
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();

        public List<JoinInfo> Resolve(List<string> tables)
        {
            if (tables.Count < 2)
            {
                return new List<JoinInfo>();
            }
            
            var results = new List<JoinInfo>();
            foreach (var table in tables)
            {
                var joins = GetJoinsForTable(table).ToList();
                foreach (var joinableTable in tables)
                {
                    var join = joins.FirstOrDefault(x => x.ForeignTable == joinableTable);
                    if (join != null)
                    {
                        results.Add(join);
                    }
                }
            }

            return results;
        }

        private IEnumerable<JoinInfo> GetJoinsForTable(string table)
        {
            using var connection = _factory.OpenSubjectDbConnection();
            return connection.Query<JoinInfo>(@"
                SELECT tc.table_name AS ThisTable,
                       kcu.column_name AS ThisColumn,
                       ccu.table_name AS ForeignTable,
                       ccu.column_name AS ForeignColumn
                FROM information_schema.table_constraints AS tc
                         JOIN information_schema.key_column_usage AS kcu
                              ON tc.constraint_name = kcu.constraint_name
                                  AND tc.table_schema = kcu.table_schema
                         JOIN information_schema.constraint_column_usage AS ccu
                              ON ccu.constraint_name = tc.constraint_name
                                  AND ccu.table_schema = tc.table_schema
                WHERE tc.constraint_type = 'FOREIGN KEY'
                  AND tc.table_name = @Table;", new { Table = table });
        }
    }
}
