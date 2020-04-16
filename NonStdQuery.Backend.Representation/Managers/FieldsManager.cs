using System.Collections.Generic;
using System.Linq;
using Dapper;
using NonStdQuery.Backend.Data.Db;
using NonStdQuery.Backend.Data.Translation;

namespace NonStdQuery.Backend.Representation.Managers
{
    public class FieldsManager
    {
        private readonly ConnectionFactory _factory = new ConnectionFactory();
        private readonly AttributeTranslator _attributeTranslator = new AttributeTranslator();

        public IEnumerable<Data.FieldInfo> GetFriendlyFields()
        {
            IEnumerable<string> results;
            using (var connection = _factory.OpenMetadataDbConnection())
            {
                 results = connection.Query<string>(@"
                    select friendly_name
                    from fields
                    where friendly_name is not null");
            }

            return results.Select(BuildFieldInfo);
        }

        private Data.FieldInfo BuildFieldInfo(string name)
        {
            var attribute = _attributeTranslator.FriendlyToReal(name);
            return new Data.FieldInfo
            {
                Name = name,
                Type = attribute.Type
            };
        }
    }
}
