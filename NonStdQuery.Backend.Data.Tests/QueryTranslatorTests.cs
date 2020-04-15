using System.Collections.Generic;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class QueryTranslatorTests
    {
        [Fact]
        public void SimpleSelect()
        {
            var translator = new QueryTranslator();
            var query = new Query
            {
                SelectAttributes = new List<string> { "Мощь империи", "Название империи" },
            };

            var dbQuery = translator.Translate(query);
            Assert.Equal("select @0.@1, @2.@3\nfrom @4;", dbQuery.Sql);
            Assert.Equal("empires", dbQuery.Parameters["@0"]);
            Assert.Equal("power", dbQuery.Parameters["@1"]);
            Assert.Equal("empires", dbQuery.Parameters["@2"]);
            Assert.Equal("name", dbQuery.Parameters["@3"]);
            Assert.Equal("empires", dbQuery.Parameters["@4"]);
        }
    }
}
