using NonStdQuery.Backend.Data.Db.Queries;
using NonStdQuery.Backend.Data.Translation;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class AttributeTranslationTests
    {
        [Fact]
        public void FriendlyToReal()
        {
            var translator = new AttributeTranslator();
            var expected = new DbAttribute
            {
                TableName = "empires",
                ColumnName = "name",
                Type = DbType.String
            };

            var actual = translator.FriendlyToReal("Название империи");
            Assert.Equal(expected, actual);
        }
    }
}
