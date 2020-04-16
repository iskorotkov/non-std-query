using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.JoinResolving;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class QueryTranslator
    {
        public DbQuery Translate(Query query)
        {
            if (query.SelectAttributes.Count == 0)
            {
                throw new ArgumentException();
            }
            
            var builder = new StringBuilder();
            var translator = new AttributeTranslator();
            var attributes = query.SelectAttributes
                .Select(translator.FriendlyToReal)
                .ToList();

            var parameters = new Dictionary<string, object>();
            var index = BuildSelectList(builder, parameters, 0, attributes);
            index = BuildFromPart(builder, parameters, index, attributes);
            index = BuildJoinList(builder, parameters, index, attributes);
            // ReSharper disable once RedundantAssignment
            index = BuildOrderByList(builder, parameters, index, query);
            
            builder.Append(";");
            return new DbQuery(builder.ToString(), parameters);
        }

        private static int BuildJoinList(StringBuilder builder, Dictionary<string, object> parameters,
            int index, List<DbAttribute> attributes)
        {
            var tables = attributes
                .Select(a => a.TableName)
                .Distinct()
                .ToList();
            var resolver = new JoinResolver();
            var joins = resolver.Resolve(tables);

            foreach (var @join in joins)
            {
                builder.Append("\njoin ");
                
                var foreignTable = "@" + index++;
                var foreignColumn = "@" + index++;
                var table = "@" + index++;
                var column = "@" + index++;
                
                parameters.Add(foreignTable, join.ForeignTable);
                parameters.Add(foreignColumn, join.ForeignColumn);
                parameters.Add(table, join.ThisTable);
                parameters.Add(column, join.ThisColumn);

                builder.Append(foreignTable);
                builder.Append(" on ");
                builder.Append(foreignTable);
                builder.Append(".");
                builder.Append(foreignColumn);
                builder.Append(" = ");
                builder.Append(table);
                builder.Append(".");
                builder.Append(column);
            }

            return index;
        }

        private static int BuildFromPart(StringBuilder builder,
            Dictionary<string, object> parameters, int index, List<DbAttribute> attributes)
        {
            builder.Append("\nfrom ");
            var tableName = "@" + index++;
            parameters.Add(tableName, attributes.First().TableName);
            builder.Append(tableName);
            return index;
        }

        private static int BuildOrderByList(StringBuilder builder,
            Dictionary<string, object> parameters, int index,
            Query query)
        {
            if (query.SortAttributes?.Count > 0)
            {
                var translator = new AttributeTranslator();
                builder.Append("\norder by");

                var orderBy = query.SortAttributes
                    .Select(translator.FriendlyToReal)
                    .ToList();

                foreach (var attribute in orderBy)
                {
                    var tableName = "@" + index++;
                    var columnName = "@" + index++;
                    parameters.Add(tableName, attribute.TableName);
                    parameters.Add(columnName, attribute.ColumnName);

                    builder.Append(tableName);
                    builder.Append(".");
                    builder.Append(columnName);
                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);
            }

            return index;
        }

        private static int BuildSelectList(StringBuilder builder,
            Dictionary<string, object> parameters, int index, List<DbAttribute> attributes)
        {
            builder.Append("select ");

            foreach (var attribute in attributes)
            {
                var tableName = "@" + index++;
                var columnName = "@" + index++;
                parameters.Add(tableName, attribute.TableName);
                parameters.Add(columnName, attribute.ColumnName);

                builder.Append(tableName);
                builder.Append(".");
                builder.Append(columnName);
                builder.Append(", ");
            }

            builder.Remove(builder.Length - 2, 2);
            return index;
        }
    }
}
