using NonStdQuery.Backend.Data.Factories;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class ExplanationManager
    {
        public ExplanationResult Explain(Query query)
        {
            var parametrizedTranslator = new QueryTranslator();
            var nonParametrizedTranslator = new QueryTranslator(new NoParametrizationTranslatorFactory());
            var parametrized = parametrizedTranslator.Translate(query);
            var nonParametrized = nonParametrizedTranslator.Translate(query);
            return new ExplanationResult
            {
                Sql = parametrized.Sql,
                PrettySql = nonParametrized.Sql,
                Parameters = parametrized.Parameters
            };
        }
    }
}
