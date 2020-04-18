using NonStdQuery.Backend.Data.Db.Queries;
using System;
using System.Text.Json;

namespace NonStdQuery.Backend.Data.Serialization
{
    public class ValueDeserializer
    {
        public object Deserialize(object value, DbType type)
        {
            if (value is JsonElement x)
            {
                return type switch
                {
                    DbType.Integer => x.GetInt64(),
                    DbType.String => x.GetString(),
                    DbType.Double => x.GetDouble(),
                    DbType.DateTime => x.GetDateTime(),
                    DbType.Bool => x.GetBoolean(),
                    _ => throw new ArgumentException()
                };
            }

            return value;
        }
    }
}
