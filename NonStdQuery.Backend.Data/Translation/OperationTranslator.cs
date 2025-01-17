﻿using System;
using System.Text;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class OperationTranslator
    {
        private readonly StringBuilder _builder;

        public OperationTranslator(StringBuilder builder)
        {
            _builder = builder;
        }

        public void Translate(Operation operation)
        {
            var value = operation switch
            {
                Operation.Equal => " = ",
                Operation.Less => " < ",
                Operation.More => " > ",
                Operation.LessEqual => " <= ",
                Operation.MoreEqual => " >= ",
                Operation.NotEqual => " <> ",
                Operation.Like => " like ",
                Operation.InsensitiveLike => " ilike ",
                Operation.NotLike => " not like ",
                Operation.NotInsensitiveLike => " not ilike ",
                _ => throw new NotImplementedException()
            };
            _builder.Append(value);
        }
    }
}
