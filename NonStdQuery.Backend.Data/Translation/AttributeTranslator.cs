using System.Linq;
using Dapper;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Db.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class AttributeTranslator
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();
        private readonly TypeTranslator _typeTranslator = new TypeTranslator();

        public DbAttribute FriendlyToReal(string attribute)
        {
            using var connection = _factory.OpenMetadataDbConnection();
            var query = connection.Query<DbAttribute>(@"
                select table_name as TableName,
                       field_name as FieldName
                from fields
                where friendly_name = @FriendlyName",
                new { FriendlyName = attribute });

             var result = query.First();
             var type = connection.Query<string>(@"
                 select data_type
                 from columns
                 where table_name = @TableName and column_name = @ColumnName",
                 new { TableName = result.TableName, ColumnName = result.ColumnName });

             result.Type = _typeTranslator.StringToType(type.First());
             return result;
        }

        public string RealToFriendly(DbAttribute attribute)
        {
            using var connection = _factory.OpenMetadataDbConnection();
            var query = connection.Query<string>(@"
                select friendly_name
                from fields
                where table_name = @TableName, field_name = @FieldName",
                new { attribute.TableName, FieldName = attribute.ColumnName });

            return query.First();
        }
    }
}
