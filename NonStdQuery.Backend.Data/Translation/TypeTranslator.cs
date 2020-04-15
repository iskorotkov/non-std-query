using System;
using System.Collections.Generic;
using NonStdQuery.Backend.Data.Db.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class TypeTranslator
    {
        private readonly List<string> _numericTypes = new List<string>
        {
            "smallint",
            "bigint",
            "integer",
            "numeric",
            "real",
            "double precision"
        };

        private readonly List<string> _boolTypes = new List<string>
        {
            "boolean",
        };

        private readonly List<string> _dateTimeTypes = new List<string>
        {
            "date",
            "time",
            "timestamp",
            "timestamp with time zone",
        };

        private readonly List<string> _stringTypes = new List<string>
        {
            "character varying",
            "varchar",
            "timestamp with time zone",
        };

        public DbType StringToType(string type)
        {
            if (_numericTypes.Contains(type))
            {
                return DbType.Numeric;
            }

            if (_boolTypes.Contains(type))
            {
                return DbType.Bool;
            }

            if (_dateTimeTypes.Contains(type))
            {
                return DbType.DateTime;
            }

            if (_stringTypes.Contains(type))
            {
                return DbType.String;
            }

            throw new ArgumentException();
        }
    }
}
