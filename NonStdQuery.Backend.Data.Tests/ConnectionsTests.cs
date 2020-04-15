using NonStdQuery.Backend.Data.Configurations;
using Xunit;

namespace NonStdQuery.Backend.Data.Tests
{
    public class ConnectionsTests
    {
        [Fact]
        public void OpenConnection()
        {
            var connections = Connections.Load();

            Assert.Equal("spaceships", connections.Subject.Database);
            Assert.Equal("localhost", connections.Subject.Host);
            Assert.Equal(5432, connections.Subject.Port);

            Assert.Equal("spaceships_meta", connections.Metadata.Database);
            Assert.Equal("localhost", connections.Metadata.Host);
            Assert.Equal(5432, connections.Metadata.Port);
        }
    }
}
