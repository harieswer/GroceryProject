using System.Data;
using ApplicationCore.DbScript;

namespace Infrastracture.Dbscript
{
    public class PostgresqlDbScript : IDbScript
    {
        public PostgresqlDbScript()
        {
            GetAllUsersById = $"SELECT * FROM f_get_all_usersbyId(@userid);";
            GetAllUsers = $"SELECT * FROM f_get_all_users()";
            comandType = CommandType.Text;
            CustomerSignDbQuery = $"SELECT * FROM fn_getCustomerDetails(@emailId,@phone_Number,@fingerprint_code)";
            PC_CustomerRegister = "insertcustomerdetails";
        }
        public string GetAllUsersById { get; }
        public string GetAllUsers { get; }
        public string CustomerSignDbQuery { get; }
        public string PC_CustomerRegister { get; }
        public CommandType comandType { get; }
    }



}
