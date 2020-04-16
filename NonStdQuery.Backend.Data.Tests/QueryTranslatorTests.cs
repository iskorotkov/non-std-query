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
                SelectAttributes = new List<string> { "Мощь империи", "Название империи" }
            };

            var dbQuery = translator.Translate(query);
            Assert.Equal("select @0.@1, @2.@3\nfrom @4;", dbQuery.Sql);
            Assert.Equal("empires", dbQuery.Parameters["@0"]);
            Assert.Equal("power", dbQuery.Parameters["@1"]);
            Assert.Equal("empires", dbQuery.Parameters["@2"]);
            Assert.Equal("name", dbQuery.Parameters["@3"]);
            Assert.Equal("empires", dbQuery.Parameters["@4"]);
        }

        [Fact]
        public void SingleIndirectJoin()
        {
            var translator = new QueryTranslator();
            var query = new Query
            {
                SelectAttributes = new List<string> { "Название империи", "Название альянса" }
            };

            var dbQuery = translator.Translate(query);
            const string sql = "select @0.@1, @2.@3\nfrom @4\njoin @5 on @5.@6 = @7.@8\njoin @9 on @9.@10 = @11.@12;";
            Assert.Equal(sql, dbQuery.Sql);
            Assert.Equal("empires", dbQuery.Parameters["@4"]);
            Assert.Equal("alliances_entries", dbQuery.Parameters["@5"]);
            Assert.Equal("alliances", dbQuery.Parameters["@9"]);
        }

        [Fact]
        public void SingleDirectJoin()
        {
            var translator = new QueryTranslator();
            var query = new Query
            {
                SelectAttributes = new List<string> { "Название империи", "Название планеты" }
            };

            var dbQuery = translator.Translate(query);
            const string sql = "select @0.@1, @2.@3\nfrom @4\njoin @5 on @5.@6 = @7.@8;";
            Assert.Equal(sql, dbQuery.Sql);
        }
    }
}
