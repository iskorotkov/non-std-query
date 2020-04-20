using System.Collections.Generic;
using System.Text;
using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Serialization;

namespace NonStdQuery.Backend.Data.Translation
{
    public class ConditionTranslator
    {
        private readonly AttributeTranslator _attributeTranslator = new AttributeTranslator();
        private readonly StringBuilder _builder;
        private readonly OperationTranslator _operationTranslator;
        private readonly LinkTranslator _linkTranslator;
        private readonly ValueDeserializer _deserializer = new ValueDeserializer();

        public Dictionary<string, object> Parameters { get; }
        public int Index { get; private set; }
        
        public ConditionTranslator(StringBuilder builder,
            Dictionary<string, object> parameters = null)
        {
            _builder = builder;
            _operationTranslator = new OperationTranslator(builder);
            _linkTranslator = new LinkTranslator(builder);

            Parameters = parameters ?? new Dictionary<string, object>();
        }

        public void Translate(IEnumerable<Condition> conditions)
        {
            foreach (var condition in conditions)
            {
                Translate(condition);
            }
        }

        public void Translate(Condition condition)
        {
            var attribute = _attributeTranslator.FriendlyToReal(condition.AttributeName);
            
            _builder.Append("\n\"");
            _builder.Append(attribute.TableName);
            _builder.Append("\".\"");
            _builder.Append(attribute.ColumnName);
            _builder.Append("\"");
            
            _operationTranslator.Translate(condition.Operation);
            TranslateConstant(_builder, condition.Value, attribute.Type);
            _linkTranslator.Translate(condition.Link);
        }

        protected virtual void TranslateConstant(StringBuilder builder, object value, DbType type)
        {
            var name = "@" + Index++;
            var param = _deserializer.Deserialize(value, type);
            Parameters.Add(name, param);
            _builder.Append(name);
        }
    }
}
