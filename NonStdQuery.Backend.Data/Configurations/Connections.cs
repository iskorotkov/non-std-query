using System.IO;
using System.Text.Json;

namespace NonStdQuery.Backend.Data.Configurations
{
    public class Connections
    {
        public Connection Subject { get; set; }
        public Connection Metadata { get; set; }

        public static Connections Load()
        {
            using var reader = new StreamReader("connections.json");
            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Connections>(json);
        }
    }
}
