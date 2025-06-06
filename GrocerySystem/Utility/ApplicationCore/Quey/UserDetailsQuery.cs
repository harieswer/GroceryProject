using ApplicationCore.Interfaces;
using ApplicationCore.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace ApplicationCore.Quey
{
    public record UserDetailsRequest : IRequest<List<User>>
    {

    }
    public record UserDetailsRequestParamater : IRequest<User>
    {
        public int UserId { set; get; }
    }

    public class UserDetailsQuery : IRequestHandler<UserDetailsRequest, List<User>>,
    IRequestHandler<UserDetailsRequestParamater, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserDetailsQuery> _logger;
        public UserDetailsQuery(IUnitOfWork unitOfWork, ILogger<UserDetailsQuery> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<List<User>?> Handle(UserDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.UserRepository.GetAllUsers();
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
        public async Task<User?> Handle(UserDetailsRequestParamater request, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.UserRepository.GetUserDetailssById(request.UserId);
            }
            catch (NpgsqlException)
            {
                return null;
            }

        }
    }
}
