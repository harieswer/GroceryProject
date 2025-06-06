using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerDetails> CustomerSigninAsync(string emailId);
        Task<int> RegisterCustomerDetails(CustomerRegisterModel registerModel);
    }
}
