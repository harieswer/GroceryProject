using System.Data.Common;
using ApplicationCore.DbScript;
using ApplicationCore.Interfaces;
using ApplicationCore.Responses;
using Dapper;
using Npgsql;

namespace Infrastracture.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection _databaseConnection;
        private readonly Func<DbTransaction> _transaction;
        private readonly IDbScript _dbScript;

        public UserRepository(DbConnection connection, Func<DbTransaction> transaction, IDbScript dbScript)
        {
            _databaseConnection = connection;
            _transaction = transaction;
            _dbScript = dbScript;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return (await _databaseConnection.QueryAsync<User>(_dbScript.GetAllUsers, commandType: _dbScript.comandType, transaction: _transaction())).ToList();
        }


        public async Task<User> GetUserDetailssById(int userId)
        {
            try
            {
                DynamicParameters parameters = new();
                parameters.Add("@userid", userId);
                return await _databaseConnection.QueryFirstOrDefaultAsync<User>(_dbScript.GetAllUsersById,
                parameters, commandType: _dbScript.comandType, transaction: _transaction());
            }

            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

    }
}
