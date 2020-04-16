using System;
using System.Text;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Backend.Data.Translation
{
    public class LinkTranslator
    {
        private readonly StringBuilder _builder;

        public LinkTranslator(StringBuilder builder)
        {
            _builder = builder;
        }

        public void Translate(LinkMethod method)
        {
            var value = method switch
            {
                LinkMethod.And => " and",
                LinkMethod.Or => " or",
                LinkMethod.None => "",
                _ => throw new NotImplementedException()
            };
            _builder.Append(value);
        }
    }
}
