using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationLib.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(List<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:ExpireTimeout")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"], // Add this line
                Audience = _configuration["Jwt:Audience"]
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ValidateToken(string jwtToken)
        {
            //private bool ValidateToken(string jwtToken, string issuer, string audience, ICollection<SecurityKey> signingKeys)


            try
            {
                byte[] signingKeys = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:SecretKey"));

                TokenValidationParameters validationParameters = new()
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeys),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"]
                };
                ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
                ClaimsPrincipal claim = tokenValidator.ValidateToken(jwtToken, validationParameters, out
                    SecurityToken _);
                string email = claim.Claims.First(s => s.Type == "emailId").Value;

                return email;
            }
            catch (Exception)
            {
                // throw new Exception("404 - Authorization failed", ex);
                return string.Empty;
            }


        }

    }
}
