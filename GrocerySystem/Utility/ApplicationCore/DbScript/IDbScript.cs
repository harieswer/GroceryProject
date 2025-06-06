using System.Data;

namespace ApplicationCore.DbScript
{
    public interface IDbScript
    {
        public string GetAllUsersById { get; }
        public string GetAllUsers { get; }
        public CommandType comandType { get; }
        public string CustomerSignDbQuery { get; }
        public string PC_CustomerRegister { get; }

    }
}
