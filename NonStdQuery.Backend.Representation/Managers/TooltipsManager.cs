using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class TooltipsManager
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();
        private readonly TypeTranslator _typeTranslator = new TypeTranslator();

        public async Task<List<Tooltip>> GetTooltips()
        {
            var results = new List<Tooltip>();

            using var metadataConnection = _factory.OpenMetadataDbConnection();
            using var subjectConnection = _factory.OpenSubjectDbConnection();
            var fields = metadataConnection
                .Query<(string TableName, string FieldName, string FriendlyName)>(@"
                    select table_name as TableName,
                           field_name as FieldName,
                           friendly_name as FriendlyName
                    from fields
                    where friendly_name is not null");

            foreach (var field in fields)
            {
                var type = await subjectConnection.QueryFirstAsync<string>(@"
                    select data_type
                    from information_schema.columns
                    where table_name = @TableName
                        and column_name = @ColumnName
                        and table_schema = 'public'",
                    new { TableName = field.TableName, ColumnName = field.FieldName });

                var translated = _typeTranslator.StringToType(type);

                var result = await subjectConnection.QueryAsync<string>($@"
                    select distinct {field.FieldName}
                    from {field.TableName};");
                
                var values = result.ToList();

                if (values.Count > 0)
                {
                    var tooltip = new Tooltip
                    {
                        FieldName = field.FriendlyName,
                        Words = values
                    };

                    results.Add(tooltip);
                }
            }

            return results;
        }
    }
}
