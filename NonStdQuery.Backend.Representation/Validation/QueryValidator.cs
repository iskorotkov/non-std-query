using System.Collections.Generic;
using System.Linq;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Backend.Representation.Validation
{
    public class QueryValidator
    {
        public bool HasErrors(Query query)
        {
            return ValidateSelect(query) != null
                   || ValidateSort(query).Count > 0
                   || ValidateConditions(query).Count > 0;
        }

        public ValidationError ValidateSelect(Query query)
        {
            return query.SelectAttributes.Count == 0
                ? new ValidationError { Message = "Не выбрано ни одного атрибута" }
                : null;
        }

        public List<AttributeError> ValidateSort(Query query)
        {
            var errors = new List<AttributeError>();
            var attributes = query.SortAttributes;
            foreach (var attribute in attributes)
            {
                if (attributes.All(x =>
                    x.AttributeName != attribute.AttributeName || x == attribute)) continue;

                if (errors.All(x => x.AttributeName != attribute.AttributeName))
                {
                    errors.Add(new AttributeError
                    {
                        AttributeName = attribute.AttributeName,
                        Message = "Данный атрибут повторяется несколько раз"
                    });
                }
            }

            return errors;
        }

        public List<AttributeError> ValidateConditions(Query query)
        {
            var errors = new List<AttributeError>();
            var length = query.Conditions.Count;
            for (var i = 0; i < length; i++)
            {
                var condition = query.Conditions[i];
                if (i < length - 1 && condition.Link == LinkMethod.None)
                {
                    errors.Add(new AttributeError
                    {
                        AttributeName = condition.AttributeName,
                        Message = "Необходимо указать связку"
                    });
                }
                else if (i == length - 1 && condition.Link != LinkMethod.None)
                {
                    errors.Add(new AttributeError
                    {
                        AttributeName = condition.AttributeName,
                        Message = "У последнего атрибута не должно быть связки"
                    });
                }
            }

            return errors;
        }
    }
}
