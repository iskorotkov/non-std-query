using System;
using NonStdQuery.Backend.Data.Configurations;
using Npgsql;

namespace NonStdQuery.Backend.Data.Db
{
    public class ConnectionFactory
    {
        private string _subjectDbConnectionString;
        private string _metadataDbConnectionString;
        private Connections _connections;

        public ConnectionFactory()
        {
            ReloadSettings();   
        }

        public void ReloadSettings()
        {
            _connections = Connections.Load();

            _subjectDbConnectionString = new NpgsqlConnectionStringBuilder
            {
                Host = _connections.Subject.Host,
                Database = _connections.Subject.Database,
                Port = _connections.Subject.Port,
                Username = Environment.GetEnvironmentVariable("PGUSER"),
                Password = Environment.GetEnvironmentVariable("PGPASSWORD")
            }.ConnectionString;

            _metadataDbConnectionString = new NpgsqlConnectionStringBuilder
            {
                Host = _connections.Metadata.Host,
                Database = _connections.Metadata.Database,
                Port = _connections.Metadata.Port,
                Username = Environment.GetEnvironmentVariable("PGUSER"),
                Password = Environment.GetEnvironmentVariable("PGPASSWORD")
            }.ConnectionString;
        }

        public NpgsqlConnection OpenSubjectDbConnection()
        {
            var connection = new NpgsqlConnection(_subjectDbConnectionString);
            connection.Open();
            return connection;
        }

        public NpgsqlConnection OpenMetadataDbConnection()
        {
            var connection = new NpgsqlConnection(_metadataDbConnectionString);
            connection.Open();
            return connection;
        }
    }
}
