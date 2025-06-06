using System.Data;
using System.Data.Common;
using DbConnectivity.Interfaces;

namespace DbConnectivity
{
    public sealed class DapperDBConnection : DbConnection
    {
        private readonly IServiceProvider provider;
        private readonly IDatabaseConnection _connection;
        public DapperDBConnection(IServiceProvider provider, IDatabaseConnection connection)
        {
            this.provider = provider;
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        public override string ConnectionString
        {
            get => _connection.Connection.ConnectionString;
            set => _connection.Connection.ConnectionString = value;
        }
        public override string Database => _connection.Connection.Database;
        public override string DataSource => _connection.Connection.DataSource;
        public override string ServerVersion => _connection.Connection.ServerVersion;
        public override ConnectionState State => _connection.State;
        public override void ChangeDatabase(string databaseName)
        {
            _connection.Connection.ChangeDatabase(databaseName);
        }
        public override void Close()
        {
            _ = _connection.Close();
        }
        public override void Open()
        {
            _connection.Connection.Open();
            //ConfigureSessionContext();
        }
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _connection.Connection.BeginTransaction(isolationLevel);
        }
        protected override DbCommand CreateDbCommand()
        {
            return _connection.Connection.CreateCommand();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _connection != null)
            {
                _connection.Dispose();
            }
            base.Dispose(disposing);
        }
        /* private void ConfigureSessionContext()
         {
             var _userContextService = provider.GetService(typeof(IUserContextService)) as UserContextService;
             int userId = _userContextService?.CurrentUser != null ? _userContextService.CurrentUser.Id : 0;
             int roleId = _userContextService?.CurrentUser != null ? _userContextService.CurrentUser.RoleId : 0;
             if (userId != 0 && roleId != 0)
             {
                 using var command = _connection.CreateCommand();
                 command.CommandText = $"EXEC sp_set_session_context 'UserId',{userId};EXEC sp_set_session_context 'RoleId',{roleId};";
                 command.ExecuteNonQuery();
             }
         }
        */
    }
}
