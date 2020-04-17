using System.Collections.Generic;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;
using Npgsql;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class ExecutionManager
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();

        public ExecutionResult Execute(Query query)
        {
            var translator = new QueryTranslator();
            var translated = translator.Translate(query);
            var result = new ExecutionResult();
            using (var connection = _factory.OpenSubjectDbConnection())
            {
                using var command = new NpgsqlCommand(translated.Sql, connection);
                foreach (var parameter in translated.Parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Data.Add(new List<string>());
                    for (var field = 0; field < reader.FieldCount; ++field)
                    {
                        var value = reader[field].ToString();
                        result.Data[result.Data.Count - 1].Add(value);
                    }
                }
            }

            return result;
        }
    }
}
