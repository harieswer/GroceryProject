using System.Net;
using System.Security.Claims;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.Responses;
using AuthenticationLib.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Quey
{
    public record CustomerSignInRequestQueryParamter : IRequest<CustomerSignInResponse>
    {
        public CustomerLoginModel? CustomerLoginModel { get; set; } = default!;
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
               
                Models.CustomerLoginResponse customerDetails = await _unitOfWork.CustomerRepository.CustomerSigninAsync(request.CustomerLoginModel);
                if (customerDetails != null)
                {
                    /*validate password*/
                    if (request.CustomerLoginModel.LoginMode == LoginMode.Password)
                    {
                        if (customerDetails.Password.Equals(request.CustomerLoginModel.Password))
                        {
                            response.AuthenticationStatus = AuthenticationStatus.Success;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response.Errors.Add("Invalid Credintials");
                            response.AuthenticationStatus = AuthenticationStatus.InvalidCredintials;
                        }
                    }

                    else if (request.CustomerLoginModel.LoginMode == LoginMode.PinNumber)
                    {
                        if (customerDetails.PinNumber.Equals(request.CustomerLoginModel.PinNumber))
                            response.AuthenticationStatus = AuthenticationStatus.Success;

                        else
                            response.AuthenticationStatus = AuthenticationStatus.InvalidPin;
                    }
                    else if (request.CustomerLoginModel.LoginMode == LoginMode.LoginWithOtp)
                    {
                        if (customerDetails.OTP.Equals(request.CustomerLoginModel.OTP))
                            response.AuthenticationStatus = AuthenticationStatus.Success;
                        else
                            response.AuthenticationStatus = AuthenticationStatus.InvalidOtp;
                    }
                    else if (request.CustomerLoginModel.LoginMode == LoginMode.FingerPrint)
                    {
                        if (customerDetails.FingerPrintCode.Equals(request.CustomerLoginModel.FingerPrintCode))
                            response.AuthenticationStatus = AuthenticationStatus.Success;
                        else
                            response.AuthenticationStatus = AuthenticationStatus.InvalidCredintials;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Errors.Add("Invalid Credintials");
                        response.AuthenticationStatus = AuthenticationStatus.Fail;
                    }
                    if(response.AuthenticationStatus==AuthenticationStatus.Success)
                    {
                        /*prpare claims*/
                        List<Claim> claims =
                        [
                             new Claim("emailId",customerDetails.Email)
                          ];
                        response.JWTToken = _tokenService.CreateToken(claims);
                        response.StatusCode=HttpStatusCode.OK;                       
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
