using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;
using NonStdQuery.Backend.Representation.Formatters;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class ExplanationManager
    {
        private readonly PrettySqlFormatter _formatter = new PrettySqlFormatter();
        
        public ExplanationResult Explain(Query query)
        {
            var translator = new QueryTranslator();
            var translated = translator.Translate(query);
            return new ExplanationResult
            {
                Sql = translated.Sql,
                PrettySql = _formatter.ToPrettySql(translated),
                Parameters = translated.Parameters
            };
        }
    }
}
