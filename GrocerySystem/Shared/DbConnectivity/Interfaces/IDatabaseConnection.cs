using System.Data;
using System.Data.Common;

namespace DbConnectivity.Interfaces
{
    public interface IDatabaseConnection : IDisposable
    {
        DbConnection Connection { get; }
        ConnectionState State { get; }
        Task Close();
        DbConnection GetConnection();
        Task OpenAsync();
        Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
    }
}
