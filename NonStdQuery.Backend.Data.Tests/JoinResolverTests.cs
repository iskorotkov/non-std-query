using System.Collections.Generic;
using NonStdQuery.Backend.Data.JoinResolving;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class JoinResolverTests
    {
        [Fact]
        public void OneTable()
        {
            var resolver = new JoinResolver();
            var result = resolver.Resolve(new List<string> { "empires" });
            Assert.Empty(result);
        }
        
        [Fact]
        public void TwoTables()
        {
            var resolver = new JoinResolver();
            var result = resolver.Resolve(new List<string> { "empires", "government_types" });
            Assert.Single(result);

            var expected = new JoinInfo
            {
                ThisTable = "empires",
                ThisColumn = "government_type_id",
                ForeignTable = "government_types",
                ForeignColumn = "id"
            };
            Assert.Equal(expected, result[0]);
        }

        [Fact]
        public void ThreeTables()
        {
            var resolver = new JoinResolver();
            var result = resolver.Resolve(
                new List<string> { "empires", "government_types", "planets" });
            
            Assert.Equal(2, result.Count);

            var first = new JoinInfo
            {
                ThisTable = "empires",
                ThisColumn = "government_type_id",
                ForeignTable = "government_types",
                ForeignColumn = "id"
            };
            var second = new JoinInfo
            {
                ThisTable = "empires",
                ThisColumn = "id",
                ForeignTable = "planets",
                ForeignColumn = "empire_id"
            };
            
            Assert.Equal(first, result[0]);
            Assert.Equal(second, result[1]);
        }
    }
}
