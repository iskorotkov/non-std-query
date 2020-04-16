using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class ExplanationManager
    {
        public ExplanationResult Explain(Query query)
        {
            var translator = new QueryTranslator();
            var translated = translator.Translate(query);
            return new ExplanationResult
            {
                Sql = translated.Sql,
                Parameters = translated.Parameters
            };
        }
    }
}
