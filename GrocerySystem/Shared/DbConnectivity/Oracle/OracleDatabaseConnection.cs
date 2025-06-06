using System.Data;
using System.Data.Common;
using DbConnectivity.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace DbConnectivity.Oracle
{
    public class OracleDatabaseConnection : IDatabaseConnection
    {
        private readonly OracleConnection _connection;

        public OracleDatabaseConnection(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
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

        public async Task Close()
        {
            await _connection.CloseAsync();
        }

        public async Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            return await _connection.BeginTransactionAsync(isolationLevel);
        }
    }
}