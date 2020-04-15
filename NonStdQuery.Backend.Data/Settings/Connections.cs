using System.Text.Json;

namespace NonStdQuery.Backend.Data.Settings
{
    public class Connections
    {
        public Connection Subject { get; set; }
        public Connection Metadata { get; set; }

        public static Connections Load()
        {
            return JsonSerializer.Deserialize<Connections>("connections.json");
        }
    }
}
