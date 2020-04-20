using NonStdQuery.Backend.Data.Db.Queries;
using System.Collections.Generic;
using System.Text;

namespace NonStdQuery.Backend.Data.Translation
{
    public class NoParametrizationConditionTranslator : ConditionTranslator
    {
        public NoParametrizationConditionTranslator(StringBuilder builder, Dictionary<string, object> parameters = null)
            : base(builder, parameters)
        {
        }

        protected override void TranslateConstant(StringBuilder builder, object value, DbType type)
        {
            switch (type)
            {
                case DbType.String:
                    builder.Append($"'{value}'");
                    break;
                case DbType.DateTime:
                    builder.Append("{ " + value + " }");
                    break;
                default:
                    builder.Append(value.ToString());
                    break;
            }
        }
    }
}
