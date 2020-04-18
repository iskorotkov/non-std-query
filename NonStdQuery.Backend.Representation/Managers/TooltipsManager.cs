using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;
using Npgsql;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class TooltipsManager
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();
        private readonly TypeTranslator _typeTranslator = new TypeTranslator();

        public async Task<List<Tooltip>> GetTooltips()
        {
            var results = new List<Tooltip>();

            await using var metadataConnection = _factory.OpenMetadataDbConnection();
            await using var subjectConnection = _factory.OpenSubjectDbConnection();
            var fields = metadataConnection
                .Query<(string TableName, string FieldName, string FriendlyName)>(@"
                    select table_name as TableName,
                           field_name as FieldName,
                           friendly_name as FriendlyName
                    from fields
                    where friendly_name is not null");

            foreach (var (tableName, fieldName, friendlyName) in fields)
            {
                var type = await subjectConnection.QueryFirstAsync<string>(@"
                    select data_type
                    from information_schema.columns
                    where table_name = @TableName
                        and column_name = @ColumnName
                        and table_schema = 'public'",
                    new { TableName = tableName, ColumnName = fieldName });

                var translatedType = _typeTranslator.StringToType(type);

                if (translatedType == DbType.Bool)
                {
                    continue;
                }

                var result = await GetValues(tableName, fieldName, translatedType, subjectConnection);
                var values = result.ToList();

                if (values.Count > 0)
                {
                    var tooltip = new Tooltip
                    {
                        FieldName = friendlyName,
                        Type = translatedType,
                        Items = values
                    };

                    results.Add(tooltip);
                }
            }

            return results;
        }

        private async Task<IEnumerable<object>> GetValues(string tableName, string fieldName, DbType translatedType,
            NpgsqlConnection subjectConnection)
        {
            switch (translatedType)
            {
                case DbType.Undefined:
                    throw new ArgumentException();
                case DbType.Integer:
                    var n = await subjectConnection.QueryAsync<long>($@"
                                              select distinct {fieldName}
                                              from {tableName}
                                              where {fieldName} is not null;");
                    return n.Select(x => (object) x);
                case DbType.String:
                    var s = await subjectConnection.QueryAsync<string>($@"
                                                select distinct {fieldName}
                                                from {tableName}
                                                where {fieldName} is not null;");
                    return s.Select(x => (object) x);
                case DbType.DateTime:
                    var dt = await subjectConnection.QueryAsync<DateTime>($@"
                                                    select distinct {fieldName}
                                                    from {tableName}
                                                    where {fieldName} is not null;");
                    return dt.Select(x => (object) x);
                case DbType.Double:
                    var d = await subjectConnection.QueryAsync<double>($@"
                                                    select distinct {fieldName}
                                                    from {tableName}
                                                    where {fieldName} is not null;");
                    return d.Select(x => (object) x);
                case DbType.Bool:
                    var b = await subjectConnection.QueryAsync<bool>($@"
                                               select distinct {fieldName}
                                               from {tableName}
                                               where {fieldName} is not null;");
                    return b.Select(x => (object) x);
                default:
                    throw new ArgumentOutOfRangeException(nameof(translatedType), translatedType, null);
            }
        }
    }
}
