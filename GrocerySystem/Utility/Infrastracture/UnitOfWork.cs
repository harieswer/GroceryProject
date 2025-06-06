using System.Data;
using System.Data.Common;
using ApplicationCore.DbScript;
using ApplicationCore.Interfaces;
using DbConnectivity.Interfaces;
using Infrastracture.Repository;

namespace Infrastracture
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDatabaseConnection dbConnection, IDbScript dbScript)
        {
            DbConnection = dbConnection;
            UserRepository = new UserRepository(dbConnection.Connection, () => Transaction, dbScript);
            CustomerRepository = new CustomerRepository(dbConnection.Connection, () => Transaction, dbScript);
        }

        public IDatabaseConnection DbConnection { get; }

        public DbTransaction? Transaction { get; private set; } = default!;

        public IUserRepository UserRepository { get; }
        public ICustomerRepository CustomerRepository { get; }


        public async Task BeginAsync(bool isTransaction = false, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (DbConnection.State != ConnectionState.Open)
            {
                await DbConnection.OpenAsync();
            }
            if (isTransaction && Transaction == null)
            {
                Transaction = await DbConnection.BeginTransactionAsync(isolationLevel);
            }
        }

        public async Task CompleteAsync()
        {
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
                Transaction?.Dispose();
                Transaction = null;
            }
            _ = (DbConnection?.Close());
        }
        public void Dispose()
        {
            Transaction?.Dispose();
            Transaction = null;
            _ = (DbConnection?.Close());
            DbConnection?.Dispose();
        }
        public async Task RollbackAsync()
        {
            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
                Transaction?.Dispose();
            }
            _ = (DbConnection?.Close());
        }
    }
}
