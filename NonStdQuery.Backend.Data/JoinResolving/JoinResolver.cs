using System.Collections.Generic;
using System.Linq;
using Dapper;
using NonStdQuery.Backend.Data.Db;

namespace NonStdQuery.Backend.Data.JoinResolving
{
    public class JoinResolver
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();

        public IEnumerable<JoinInfo> Resolve(List<string> tables)
        {
            if (tables.Count < 2)
            {
                return new List<JoinInfo>();
            }

            var firstTable = tables[0];
            tables.RemoveAt(0);
            return TryToJoin(firstTable, new List<string>(), tables)
                .Distinct();
        }

        private IEnumerable<JoinInfo> TryToJoin(string table, List<string> traversed,
            List<string> tablesToJoin)
        {
            if (traversed.Contains(table))
            {
                yield break;
            }
            traversed.Add(table);

            var joins = GetJoinsForTable(table).ToList();
            foreach (var @join in joins)
            {
                if (tablesToJoin.Count == 0)
                {
                    yield break;
                }
                
                if (traversed.Contains(@join.ForeignTable))
                {
                    continue;
                }

                if (tablesToJoin.Contains(join.ForeignTable))
                {
                    tablesToJoin.Remove(join.ForeignTable);
                    yield return join;
                }
            }

            foreach (var @join in joins)
            {
                if (tablesToJoin.Count == 0)
                {
                    yield break;
                }
                
                if (traversed.Contains(@join.ForeignTable))
                {
                    continue;
                }

                var recursiveJoins = TryToJoin(join.ForeignTable, traversed, tablesToJoin).ToList();
                if (recursiveJoins.Count > 0)
                {
                    tablesToJoin.Remove(join.ForeignTable);
                    yield return join;
                    foreach (var recursiveJoin in recursiveJoins)
                    {
                        yield return recursiveJoin;
                    }
                }
            }
        }

        private IEnumerable<JoinInfo> GetJoinsForTable(string table)
        {
            List<JoinInfo> joins;
            using (var connection = _factory.OpenSubjectDbConnection())
            {
                joins = connection.Query<JoinInfo>(@"
                    SELECT tc.table_name   AS thistable,
                           kcu.column_name AS thiscolumn,
                           ccu.table_name  AS foreigntable,
                           ccu.column_name AS foreigncolumn
                    FROM information_schema.table_constraints AS tc
                             JOIN information_schema.key_column_usage AS kcu
                                  ON tc.constraint_name = kcu.constraint_name
                                      AND tc.table_schema = kcu.table_schema
                             JOIN information_schema.constraint_column_usage AS ccu
                                  ON ccu.constraint_name = tc.constraint_name
                                      AND ccu.table_schema = tc.table_schema
                    WHERE tc.constraint_type = 'FOREIGN KEY'
                      AND (ccu.table_name = @Table
                        OR tc.table_name = @Table);",
                        new { Table = table })
                    .ToList();
            }

            foreach (var @join in joins)
            {
                if (join.ForeignTable == table)
                {
                    join.Reverse();
                }
            }

            return joins;
        }
    }
}
