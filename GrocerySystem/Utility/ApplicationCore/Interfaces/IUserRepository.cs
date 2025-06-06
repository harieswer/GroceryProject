using ApplicationCore.Responses;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserDetailssById(int userId);

    }
}
