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
            Assert.Equal("select \"empires\".\"power\", \"empires\".\"name\"\nfrom \"empires\";", dbQuery.Sql);
            Assert.Empty(dbQuery.Parameters);
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
            const string sql = "select \"empires\".\"name\", \"alliances\".\"name\"\n" +
                               "from \"empires\"\n" +
                               "join \"alliances_entries\" on \"alliances_entries\".\"empire_id\" = \"empires\".\"id\"\n" +
                               "join \"alliances\" on \"alliances\".\"id\" = \"alliances_entries\".\"alliance_id\";";
            Assert.Equal(sql, dbQuery.Sql);
            Assert.Empty(dbQuery.Parameters);
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
            const string sql = "select \"empires\".\"name\", \"planets\".\"name\"\n" +
                               "from \"empires\"\n" +
                               "join \"planets\" on \"planets\".\"empire_id\" = \"empires\".\"id\";";
            Assert.Equal(sql, dbQuery.Sql);
            Assert.Empty(dbQuery.Parameters);
        }

        [Fact]
        public void Sorting()
        {
            var translator = new QueryTranslator();
            var query = new Query
            {
                SelectAttributes = new List<string> { "Название империи", "Название планеты" },
                SortAttributes = new List<string> { "Название империи", "Мощь империи" }
            };
            var dbQuery = translator.Translate(query);
            var sql = "select \"empires\".\"name\", \"planets\".\"name\"\n" +
                      "from \"empires\"\n" +
                      "join \"planets\" on \"planets\".\"empire_id\" = \"empires\".\"id\"\n" +
                      "order by \"empires\".\"name\", \"empires\".\"power\";";
            Assert.Equal(sql, dbQuery.Sql);
        }
    }
}
