using NonStdQuery.Backend.Data.Translation;
using System.Collections.Generic;
using System.Text;

namespace NonStdQuery.Backend.Data.Factories
{
    public class NoParametrizationTranslatorFactory : ConditionTranslatorFactory
    {
        public override ConditionTranslator Create(StringBuilder builder, Dictionary<string, object> parameters = null)
        {
            return new NoParametrizationConditionTranslator(builder, parameters);
        }
    }
}
