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
            return GetFriendlyNames().Select(BuildFieldInfo);
        }

        private IEnumerable<string> GetFriendlyNames()
        {
            using var connection = _factory.OpenMetadataDbConnection();
            return connection.Query<string>(@"
                    select friendly_name
                    from fields
                    where friendly_name is not null");
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
