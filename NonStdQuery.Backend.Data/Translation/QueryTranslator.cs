using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NonStdQuery.Backend.Data.Db.Queries;
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
            builder.Append("select ");

            var translator = new AttributeTranslator();
            var attributes = query.SelectAttributes
                .Select(translator.FriendlyToReal)
                .ToList();

            var parameters = new Dictionary<string, object>();
            var index = 0;

            string tableName;
            string columnName;
            
            foreach (var attribute in attributes)
            {
                tableName = "@" + index++;
                columnName = "@" + index++;
                parameters.Add(tableName, attribute.TableName);
                parameters.Add(columnName, attribute.ColumnName);

                builder.Append(tableName);
                builder.Append(".");
                builder.Append(columnName);
                builder.Append(", ");
            }

            builder.Remove(builder.Length - 2, 2);
            builder.Append("\nfrom ");

            tableName = "@" + index++;
            parameters.Add(tableName, attributes.First().TableName);
            builder.Append(tableName);

            // TODO: join tables

            if (query.SortAttributes?.Count > 0)
            {
                builder.Append("\norder by");

                var orderBy = query.SortAttributes
                    .Select(translator.FriendlyToReal)
                    .ToList();
                
                foreach (var attribute in orderBy)
                {
                    tableName = "@" + index++;
                    columnName = "@" + index++;
                    parameters.Add(tableName, attribute.TableName);
                    parameters.Add(columnName, attribute.ColumnName);

                    builder.Append(tableName);
                    builder.Append(".");
                    builder.Append(columnName);
                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);
            }
            
            builder.Append(";");
            return new DbQuery(builder.ToString(), parameters);
        }
    }
}
