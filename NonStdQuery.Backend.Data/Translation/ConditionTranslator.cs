using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class ConditionTranslator
    {
        private readonly AttributeTranslator _attributeTranslator = new AttributeTranslator();
        private readonly StringBuilder _builder;
        private readonly OperationTranslator _operationTranslator;
        private readonly LinkTranslator _linkTranslator;

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
            TranslateConstant(condition, attribute);
            _linkTranslator.Translate(condition.Link);
        }

        private void TranslateConstant(Condition condition, DbAttribute attribute)
        {
            var name = "@" + Index++;
            var value = CastValue(condition, attribute);
            Parameters.Add(name, value);
            _builder.Append(name);
        }

        private object CastValue(Condition condition, DbAttribute attribute)
        {
            if (condition.Value is JsonElement value)
            {
                return attribute.Type switch
                {
                    DbType.Numeric => value.GetInt32(),
                    DbType.String => value.GetString(),
                    DbType.DateTime => value.GetDateTime(),
                    DbType.Bool => value.GetBoolean(),
                    _ => throw new ArgumentException()
                };
            }

            return condition.Value;
        }
    }
}
