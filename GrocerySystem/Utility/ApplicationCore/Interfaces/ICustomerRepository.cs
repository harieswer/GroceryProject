using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerLoginResponse> CustomerSigninAsync(CustomerLoginModel customerLoginModel);
        Task<int> RegisterCustomerDetails(CustomerRegisterModel registerModel);
    }
}
