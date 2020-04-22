using System;
using System.Collections.Generic;
using NonStdQuery.Backend.Data.Db.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class TypeTranslator
    {
        private readonly List<string> _integerTypes = new List<string>
        {
            "smallint",
            "bigint",
            "integer",
        };

        private readonly List<string> _doubleTypes = new List<string>
        {
            "real",
            "double precision"
        };

        private readonly List<string> _decimalTypes = new List<string>
        {
            "numeric"
        };

        private readonly List<string> _boolTypes = new List<string>
        {
            "boolean",
        };

        private readonly List<string> _dateTimeTypes = new List<string>
        {
            "date"
        };

        private readonly List<string> _stringTypes = new List<string>
        {
            "character varying",
            "varchar"
        };

        // TODO: Add time and datetime types
        // "time", "timestamp", "timestamp with time zone", ...

        public DbType StringToType(string type)
        {
            if (_integerTypes.Contains(type))
            {
                return DbType.Integer;
            }

            if (_boolTypes.Contains(type))
            {
                return DbType.Bool;
            }

            if (_decimalTypes.Contains(type))
            {
                return DbType.Decimal;
            }

            if (_doubleTypes.Contains(type))
            {
                return DbType.Double;
            }

            if (_dateTimeTypes.Contains(type))
            {
                return DbType.Date;
            }

            if (_stringTypes.Contains(type))
            {
                return DbType.String;
            }

            throw new ArgumentException();
        }
    }
}
