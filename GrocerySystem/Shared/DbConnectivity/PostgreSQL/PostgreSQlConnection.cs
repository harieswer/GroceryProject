using System.Data;
using System.Data.Common;
using DbConnectivity.Interfaces;
using Npgsql;

namespace DbConnectivity.PostgreSQL
{
    public class PostgreSQLConnection : IDatabaseConnection
    {
        private readonly NpgsqlConnection _connection;

        public PostgreSQLConnection(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        public DbConnection Connection => _connection;

        public ConnectionState State => _connection.State;

        public DbConnection GetConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }
        public async Task OpenAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }


        public void Dispose()
        {
            _connection?.Dispose();
        }
        public async Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            return await _connection.BeginTransactionAsync(isolationLevel);
        }

        public async Task Close()
        {
            await _connection.CloseAsync();
        }
    }
}
