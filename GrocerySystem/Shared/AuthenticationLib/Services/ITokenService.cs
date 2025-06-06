using System.Security.Claims;

namespace AuthenticationLib.Services
{
    public interface ITokenService
    {
        public string CreateToken(List<Claim> claims);

        public string ValidateToken(string token);

    }

}
