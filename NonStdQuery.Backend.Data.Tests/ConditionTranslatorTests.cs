using System.Text;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Data.Translation;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class ConditionTranslatorTests
    {
        [Fact]
        public void SingleCondition()
        {
            var builder = new StringBuilder();
            var translator = new ConditionTranslator(builder);
            var condition = new Condition
            {
                AttributeName = "Название империи",
                Operation = Operation.NotEqual,
                Value = "Империя 1"
            };
            translator.Translate(condition);

            Assert.Equal("\n\"empires\".\"name\" <> @0", builder.ToString());
            Assert.Single(translator.Parameters);
            Assert.Equal("Империя 1", translator.Parameters["@0"]);
        }

        [Fact]
        public void SeveralCondition()
        {
            var builder = new StringBuilder();
            var translator = new ConditionTranslator(builder);

            translator.Translate(new Condition
            {
                AttributeName = "Название империи",
                Operation = Operation.Equal,
                Value = "Империя 1",
                Link = LinkMethod.And
            });

            translator.Translate(new Condition
            {
                AttributeName = "Название планеты",
                Operation = Operation.LessEqual,
                Value = "Планета 1",
                Link = LinkMethod.Or
            });
            
            translator.Translate(new Condition
            {
                AttributeName = "Мощь империи",
                Operation = Operation.More,
                Value = 50,
            });

            Assert.Equal("\n\"empires\".\"name\" = @0 and" +
                         "\n\"planets\".\"name\" <= @1 or" +
                         "\n\"empires\".\"power\" > @2",
                builder.ToString());
            
            Assert.Equal(3, translator.Parameters.Count);
            Assert.Equal("Империя 1", translator.Parameters["@0"]);
            Assert.Equal("Планета 1", translator.Parameters["@1"]);
            Assert.Equal(50, translator.Parameters["@2"]);
        }
    }
}
