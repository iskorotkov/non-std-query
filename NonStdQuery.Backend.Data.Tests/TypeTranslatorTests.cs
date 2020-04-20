using System;
using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.Translation;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class TypeTranslatorTests
    {
        [Fact]
        public void CommonTypes()
        {
            var translator = new TypeTranslator();
            
            Assert.Equal(DbType.Integer, translator.StringToType("integer"));
            Assert.Equal(DbType.Bool, translator.StringToType("boolean"));
            Assert.Equal(DbType.String, translator.StringToType("varchar"));
            Assert.Equal(DbType.Date, translator.StringToType("date"));
            Assert.Throws<ArgumentException>(() => translator.StringToType("custom type"));
        }
    }
}
