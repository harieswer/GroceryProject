using System.Text;
using AuthenticationLib.MiddleWare;
using AuthenticationLib.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AuthenticationLib.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection RegisterAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            string? authenticationModel = configuration.GetValue<string>("AuthenticationModel");
            switch (authenticationModel)
            {
                case "jwt":

                    _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.TokenValidationParameters = new TokenValidationParameters
                          {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidateIssuerSigningKey = true,
                              ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                              ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey")))
                          };
                      });

                    _ = services.AddSingleton<ITokenService, JwtTokenService>();
                    _ = services.AddAuthenticationSwaggerMiddleWare();
                    break;

            }
            return services;
        }

        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder, IConfiguration configuration)
        {
            string? authenticationModel = configuration.GetValue<string>("AuthenticationModel");
            switch (authenticationModel)
            {
                case "jwt":
                    _ = builder.UseMiddleware<JWTAuthenticationMiddleWare>();
                    break;
            }
            return builder;
        }

        public static IServiceCollection AddAuthenticationSwaggerMiddleWare(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Utility API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            return services;

        }
    }
}
