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
            BuildSelectList(builder, attributes);
            BuildFromPart(builder, attributes);
            BuildJoinList(builder, attributes);
            BuildOrderByList(builder, query.SortAttributes);
            builder.Append(";");

            return new DbQuery(builder.ToString(), parameters);
        }

        private static void BuildJoinList(StringBuilder builder, List<DbAttribute> attributes)
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

                builder.Append("\"");
                builder.Append(join.ForeignTable);
                builder.Append("\" on \"");
                builder.Append(join.ForeignTable);
                builder.Append("\".\"");
                builder.Append(join.ForeignColumn);
                builder.Append("\" = \"");
                builder.Append(join.ThisTable);
                builder.Append("\".\"");
                builder.Append(join.ThisColumn);
                builder.Append("\"");
            }
        }

        private static void BuildFromPart(StringBuilder builder, List<DbAttribute> attributes)
        {
            builder.Append("\nfrom \"");
            builder.Append(attributes[0].TableName);
            builder.Append("\"");
        }

        private static void BuildOrderByList(StringBuilder builder, List<SortAttribute> sortAttributes)
        {
            if (sortAttributes?.Count > 0)
            {
                var translator = new AttributeTranslator();
                builder.Append("\norder by ");

                var orderBy = sortAttributes
                    .Select(x => new { x.Ascending, Attribute = translator.FriendlyToReal(x.AttributeName) })
                    .ToList();

                foreach (var attribute in orderBy)
                {
                    builder.Append("\"");
                    builder.Append(attribute.Attribute.TableName);
                    builder.Append("\".\"");
                    builder.Append(attribute.Attribute.ColumnName);
                    builder.Append("\"");

                    builder.Append(attribute.Ascending ? " asc" : " desc");

                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);
            }
        }

        private static void BuildSelectList(StringBuilder builder, List<DbAttribute> attributes)
        {
            builder.Append("select ");

            foreach (var attribute in attributes)
            {
                builder.Append("\"");
                builder.Append(attribute.TableName);
                builder.Append("\".\"");
                builder.Append(attribute.ColumnName);
                builder.Append("\", ");
            }

            builder.Remove(builder.Length - 2, 2);
        }
    }
}
