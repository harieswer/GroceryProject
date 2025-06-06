using System.Data;
using System.Data.Common;
using DbConnectivity.Interfaces;

namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task RollbackAsync();
        Task BeginAsync(bool isTransaction = false,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CompleteAsync();
        IDatabaseConnection DbConnection { get; }
        DbTransaction Transaction { get; }
        IUserRepository UserRepository { get; }
        ICustomerRepository CustomerRepository { get; }
    }
}
