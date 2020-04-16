using System.Collections.Generic;
using System.Linq;
using Dapper;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;

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
                result.Data = connection.Query<List<string>>(translated.Sql, translated.Parameters).ToList();
            }

            return result;
        }
    }
}
