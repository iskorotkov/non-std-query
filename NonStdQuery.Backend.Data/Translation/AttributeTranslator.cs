﻿using System.Linq;
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
            DbAttribute result;
            using (var connection = _factory.OpenMetadataDbConnection())
            {
                var query = connection.Query<DbAttribute>(@"
                    select table_name as TableName,
                           field_name as ColumnName
                    from fields
                    where friendly_name = @FriendlyName",
                    new { FriendlyName = attribute });

                result = query.First();
            }

            using (var connection = _factory.OpenSubjectDbConnection())
            {
                var type = connection.Query<string>(@"
                    select data_type
                    from information_schema.columns
                    where table_name = @TableName and column_name = @ColumnName",
                    new { TableName = result.TableName, ColumnName = result.ColumnName });

                result.Type = _typeTranslator.StringToType(type.First());
            }

            return result;
        }

        public string RealToFriendly(DbAttribute attribute)
        {
            using var connection = _factory.OpenMetadataDbConnection();
            var query = connection.Query<string>(@"
                select friendly_name
                from fields
                where table_name = @TableName and field_name = @FieldName",
                new { attribute.TableName, FieldName = attribute.ColumnName });

            return query.First();
        }
    }
}
