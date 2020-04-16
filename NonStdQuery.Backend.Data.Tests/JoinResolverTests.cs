using System.Collections.Generic;
using System.Linq;
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
            var result = resolver.Resolve(
                new List<string> { "empires", "government_types" })
                .ToList();
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
                new List<string> { "empires", "government_types", "planets" })
                .ToList();
            
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

        [Fact]
        public void IndirectJoins()
        {
            var resolver = new JoinResolver();
            var joins = resolver.Resolve(new List<string> { "empires", "alliances" }).ToList();
            Assert.Equal(2, joins.Count);
            
            Assert.Equal("empires", joins[0].ThisTable);
            Assert.Equal("id", joins[0].ThisColumn);
            Assert.Equal("alliances_entries", joins[0].ForeignTable);
            Assert.Equal("empire_id", joins[0].ForeignColumn);

            Assert.Equal("alliances_entries", joins[1].ThisTable);
            Assert.Equal("alliance_id", joins[1].ThisColumn);
            Assert.Equal("alliances", joins[1].ForeignTable);
            Assert.Equal("id", joins[1].ForeignColumn);
        }
    }
}
