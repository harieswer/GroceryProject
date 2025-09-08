using System.Data;
using System.Data.Common;
using ApplicationCore.DbScript;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Dapper;

namespace Infrastracture.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbConnection _databaseConnection;
        private readonly Func<DbTransaction> _transaction;
        private readonly IDbScript _dbScript;
        public CustomerRepository(DbConnection connection, Func<DbTransaction> transaction, IDbScript dbScript)
        {
            _databaseConnection = connection;
            _transaction = transaction;
            _dbScript = dbScript;
        }
        public async Task<CustomerLoginResponse> CustomerSigninAsync(CustomerLoginModel customerLoginModel)
        {
            DynamicParameters parameters = new();
            parameters.Add("@emailId", customerLoginModel.Email);
            parameters.Add("@phone_Number", customerLoginModel.PhoneNumber);
            parameters.Add("@Imi_Number", customerLoginModel.IMINumber);
            return await _databaseConnection.QueryFirstOrDefaultAsync<CustomerLoginResponse>(_dbScript.CustomerSignDbQuery, parameters, commandType: _dbScript.comandType, transaction: _transaction());
        }


        public async Task<int> RegisterCustomerDetails(CustomerRegisterModel registerModel)
        {
            DynamicParameters parameters = new();      
            parameters.Add("@ip_phnumber", registerModel.PhoneNumber);
            parameters.Add("@ip_email", registerModel.Email);
            parameters.Add("@ip_pwd", registerModel.Password);
            parameters.Add("@ip_fristname", registerModel.FristName);
            parameters.Add("@ip_lastname", registerModel.LastName);
            parameters.Add("@op_customerid", dbType: DbType.Int32,
            direction: ParameterDirection.Output);
            _ = await _databaseConnection.ExecuteAsync(_dbScript.PC_CustomerRegister, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction());
            int result = parameters.Get<int>("@op_customerid");
            return result;
        }
    }
}
