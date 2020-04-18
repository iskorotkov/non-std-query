using NonStdQuery.Backend.Representation.Managers;
using Xunit;

namespace NonStdQuery.Backend.Representation.Tests
{
    public class TooltipsManagerTests
    {
        [Fact]
        public void GetTooltips()
        {
            var manager = new TooltipsManager();
            var tooltips = manager.GetTooltips()
                .GetAwaiter().GetResult();

            Assert.NotEmpty(tooltips);

            foreach (var tooltip in tooltips)
            {
                Assert.NotEmpty(tooltip.FieldName);
                Assert.NotEmpty(tooltip.Words);
            }
        }
    }
}
