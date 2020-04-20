using System.Collections.Generic;
using System.Text;
using NonStdQuery.Backend.Data.Translation;

namespace NonStdQuery.Backend.Data.Factories
{
    public class ConditionTranslatorFactory
    {
        public virtual ConditionTranslator Create(StringBuilder builder, Dictionary<string, object> parameters = null)
        {
            return new ConditionTranslator(builder, parameters);
        }
    }
}
