using AuthenticationLib.Services;
using Microsoft.AspNetCore.Http;

namespace AuthenticationLib.MiddleWare
{
    public class JWTAuthenticationMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly List<string> _ignoreUrls =
        [  "/api/customer/customer-login"
        ];
        public JWTAuthenticationMiddleWare(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpRequest request = httpContext.Request;
            string incomingRequestUrl = httpContext.Request.Path;
            string token = string.Empty;
            bool isValidToken = false;
            if (_ignoreUrls.Any(s => s.Equals(incomingRequestUrl, StringComparison.CurrentCultureIgnoreCase)))
            {
                await _next(httpContext);
            }
            else
            {
                Microsoft.Extensions.Primitives.StringValues header = httpContext.Request.Headers["Authorization"];
                if (header.Count == 0)
                {
                    throw new Exception("Authorization header is empty");
                }

                string[] tokenValue = Convert.ToString(header).Trim().Split(" ");
                if (tokenValue.Length > 1)
                {
                    token = tokenValue[1];
                }

                string email = _tokenService.ValidateToken(token);
                isValidToken = !string.IsNullOrWhiteSpace(email);
                if (!isValidToken)
                {
                    throw new Exception("Invalid credintials");
                }
                await _next(httpContext);
            }
        }
    }



}
