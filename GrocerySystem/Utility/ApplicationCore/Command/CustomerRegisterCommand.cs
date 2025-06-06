using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Command
{
    public record CustomerRegisterRequestParameter : IRequest<int>
    {
        public required CustomerRegisterModel CustomerRegisterModel { get; set; }
    }
    public class CustomerRegisterCommand : IRequestHandler<CustomerRegisterRequestParameter, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerRegisterCommand> _logger;
        public CustomerRegisterCommand(IUnitOfWork unitOfWork, ILogger<CustomerRegisterCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(CustomerRegisterRequestParameter request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CustomerRepository.RegisterCustomerDetails(request.CustomerRegisterModel);
        }
    }
}
