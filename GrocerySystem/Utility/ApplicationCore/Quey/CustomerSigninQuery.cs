using System.Net;
using System.Security.Claims;
using ApplicationCore.Interfaces;
using ApplicationCore.Responses;
using AuthenticationLib.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Quey
{
    public record CustomerSignInRequestQueryParamter : IRequest<CustomerSignInResponse>
    {
        public string Email { set; get; } = default!;
        public string Password { set; get; } = default!;
    }

    public class CustomerSigninQuery : IRequestHandler<CustomerSignInRequestQueryParamter, CustomerSignInResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerSigninQuery> _logger;
        private readonly IConfiguration _configuration;
        public CustomerSigninQuery(ITokenService tokenService, IUnitOfWork unitOfWork, ILogger<CustomerSigninQuery> logger, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<CustomerSignInResponse> Handle(CustomerSignInRequestQueryParamter request, CancellationToken cancellationToken)
        {
            CustomerSignInResponse response = new();
            try
            {
                Models.CustomerDetails customerDetails = await _unitOfWork.CustomerRepository.CustomerSigninAsync(request.Email);
                if (customerDetails != null)
                {
                    /*validate password*/
                    if (customerDetails.Password.Equals(request.Password))
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Email = request.Email;
                        /*prpare claims*/
                        List<Claim> claims =
                        [
                             new Claim("emailId",customerDetails.Email)
                          ];
                        response.JWTToken = _tokenService.CreateToken(claims);
                        response.AuthenticationStatus = AuthenticationStatus.Success;


                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Errors.Add("Invalid Credintials");
                        response.AuthenticationStatus = AuthenticationStatus.Fail;

                    }

                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Errors.Add("Invalid Credintials");
                    response.AuthenticationStatus = AuthenticationStatus.InvalidCredintials;

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.AuthenticationStatus = AuthenticationStatus.InvalidCredintials;

                response.Errors.Add(ex.Message);
            }
            return response;
        }


    }
}
