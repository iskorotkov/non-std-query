using System;
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
            
            foreach (var attribute in attributes)
            {
                builder.Append(attribute.ColumnName);
                builder.Append(", ");
            }

            builder.Remove(builder.Length - 2, 2);

            builder.Append("\nfrom ");
            builder.Append(attributes.First().TableName);

            // TODO: join tables

            if (query.SortAttributes?.Count > 0)
            {
                builder.Append("\norder by");

                var orderBy = query.SortAttributes
                    .Select(translator.FriendlyToReal)
                    .ToList();
                foreach (var attribute in orderBy)
                {
                    builder.Append(attribute.ColumnName);
                    builder.Append(", ");
                }

                builder.Remove(builder.Length - 2, 2);
            }
            
            builder.Append(";");
            return new DbQuery(builder.ToString());
        }
    }
}
